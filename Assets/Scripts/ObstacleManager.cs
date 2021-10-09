using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Manager obstacle yang bertugas melakukan spawn obstacle
    // Script versi problem 8
    // addition:
    // - Menambahkan bool apakah object yang di destroy akan di spawn kembali untuk pengimplementasian spawn ulang object agar problem sebelumnya tidak terpengaruh
    // - Menambah variabel jumlah obstacle yang ingin di spawn dengan default 0. Jika tidak diubah, maka jumlah obstacle yang di spawn akan tetap random dari 3-10
    // - Menambahkan variabel waktu spawn kembali obstacle yang telah di destroy. Nilai default 3
    // - Menambahkan kondisi di update yang mengecek apakah ada obstacle yang telah di destroy. Jika ada, maka akan dipanggil fungsi untuk spawn kembali obstacle
    // - Menambahkan fungsi coroutine SpawnDestroyedObstacle(GameObject, int) yang akan spawn obstacle kembali dengan posisi random dengan delay waktu yang ditentukan

    // Menyimpan batas-batas area game
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    [SerializeField] private Transform topBorder;
    [SerializeField] private Transform bottomBorder;

    // Untuk menyimpan batas area spawn
    private float min_x;
    private float min_y;
    private float max_x;
    private float max_y;

    private float area_padding = 0.4f;

    // Menyimpan berapa obstacle yang akan di spawn di awal. Jika nilai kurang dari sama dengan 0, maka jumlah akan tetap random dari 3-10 
    [SerializeField] private int obstacleCount = 0;

    // Waktu spawn back time obstacle yang telah di destroy sebelumnya
    [SerializeField] private int spawnBackTime = 3;

    // Menyimpan apakah obstacle destroyable, di set false secara default untuk problem dimana obstacle masih tidak destroyable. Untuk problem selanjutnya diset true manual pada editor
    [SerializeField] private bool destroyableObstacle = false;

    // Menyimpan apakah obstacle yang telah di destroy akan di spawn kembali kemudian. Akan dipakai seperti variabel destroyableObstacle
    [SerializeField] private bool spawnDestroyed = false;

    // List untuk destroyed object
    private List<GameObject> destroyedObstacle;

    // Untuk factory pattern
    [SerializeField]
    MonoBehaviour obstacleFactory;
    IFactory Factory { get { return obstacleFactory as IFactory; } }

    //Untuk menjadikan object singleton
    private static ObstacleManager _instance = null;
    public static ObstacleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObstacleManager>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Init list untuk destroyed obstacle nanti
        destroyedObstacle = new List<GameObject>();

        // Mendapatkan batas area spawn
        min_x = leftBorder.position.x + area_padding;
        min_y = bottomBorder.position.y + area_padding;
        max_x = rightBorder.position.x - area_padding;
        max_y = topBorder.position.y - area_padding;

        // Objek akan di spawn dengan jumlah random dengan range 3 sampai 10, jika jumlah tidak ditentukan sebelumnya (masih 0)
        if (obstacleCount <= 0)
            obstacleCount = Random.Range(3, 11);
        Debug.Log("Obstacle Spawned: " + obstacleCount);
        for (int i = 0; i < obstacleCount; i++)
        {
            SpawnRandom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Dicek setiap update apakah ada obstacle yang telah di destroy untuk di spawn kembali
        if (spawnDestroyed && destroyedObstacle.Count > 0)
        {
            // Dari list yang menyimpan destroyed obstacle, obstacle diremove dari list dan akan di spawn kembali dalam waktu 3 detik
            GameObject obstacle = destroyedObstacle[0];
            destroyedObstacle.RemoveAt(0);
            // Coroutine untuk spawn kembali objek dengan posisi random dan delay sesuai waktu yang dimasukkan
            StartCoroutine(SpawnDestroyedObstacle(obstacle, spawnBackTime));
        }
    }

    void SpawnRandom()
    {
        // Fungsi untuk spawn objek obstacle dengan posisi random dalam area

        // Untuk mendapatkan posisi spawn secara random dalam area
        Vector3 spawn_pos = GetRandomCoordinate();

        // Jika area spawn collide dengan object lain, maka object akan di spawn ulang
        if (CheckSpawnCollide(spawn_pos, 0.3f))
        {
            SpawnRandom();
            return;
        }

        // Memanggil metode untuk membuat obstacle dan menyimpan obstacle di variabel
        GameObject obstacle = Factory.CreateRandomObject(spawn_pos);
    }

    private Vector3 GetRandomCoordinate()
    {
        // Fungsi untuk mendapatkan random coordinate dalam game area sebagai titik spawn
        Vector3 spawn_pos = new Vector3
        {
            x = Random.Range(min_x * 100, max_x * 100) / 100,
            y = Random.Range(min_y * 100, max_y * 100) / 100,
            z = 0
        };

        return spawn_pos;
    }

    private bool CheckSpawnCollide(Vector3 spawn_pos, float radius)
    {
        // Fungsi untuk mengecek apakah posisi tempat spawn akan collide dengan object lain

        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawn_pos, radius);

        // Jika area sekitar posisi spawn collide dengan object lain, maka akan return true. Jika tidak, akan return false
        return (colliders.Length > 0);
    }

    public void HitObstacle(GameObject obstacle)
    {
        // Fungsi untuk memanggil fungsi menghancurkan obstacle dan menambahkan score, akan dipanggil saat kondisi circle collide dengan obstacle dll.

        // Jika obstacle tidak destroyable, maka fungsi tidak jadi dipanggil
        if (!destroyableObstacle)
            return;

        DestroyObstacle(obstacle);
        GameManager.Instance.IncreaseScore();
    }

    private void DestroyObstacle(GameObject obstacle)
    {
        // Fungsi untuk menghancurkan object. Namun sebenarnya object hanya di nonaktifkan

        obstacle.SetActive(false);
        // Setelah object dinonaktifkan, dimasukkan ke dalam list yang menyimpan destroyed object 
        destroyedObstacle.Add(obstacle);
    }

    private IEnumerator SpawnDestroyedObstacle(GameObject obstacle, int time)
    {
        // Fungsi coroutine untuk spawn kembali objectb yang telah dinonaktifkan sebelumnya dengan posisi baru yang random, dan delay time
        
        yield return new WaitForSeconds(time);

        // Untuk mengecek apakah area spawn yang didapatkan masih belum clear atau sudah. Jika belum maka akan dicari posisi lain untuk spawn sampai dapat
        bool spawnNotClear = false;
        Vector3 spawn_pos = GetRandomCoordinate();
        while (!spawnNotClear)
        {
            // Jika area spawn di cek terdapat collide, akan dicari posisi random baru
            if (CheckSpawnCollide(spawn_pos, 0.3f))
            {
                spawn_pos = GetRandomCoordinate();
                continue;
            }
            // Jika tidak ada collide, maka area spawn sudah clear
            spawnNotClear = true;
        }

        // Mengganti posisi objek menjadi posisi baru, lalu objek kembali diaktifkan
        obstacle.transform.position = spawn_pos;
        obstacle.SetActive(true);
        //Debug.Log("Object spawned back after " + time + " seconds");
    }

    // referensi:
    //- code dari tugas 7 membuat game survival shooter
}
