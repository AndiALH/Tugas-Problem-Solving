using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Manager obstacle yang bertugas melakukan spawn obstacle
    // Script versi problem 7
    // addition:
    // - Membuat scipt menjadi singleton
    // - Menambahkan bool apakah object destroyable untuk pengimplementasian menghilangkan object agar problem sebelumnya tidak terpengaruh
    // - Memasukkan fungsi mendapatkan random position menjadi sebuah method yaitu GetRandomCoordinate(), karena mungkin akan dipakai lagi selanjutnya
    // - Memasukkan fungsi mengecek apakah area spawn akan collide menjadi sebuah method yaitu CheckSpawnCollide(Vector3, float), karena mungkin akan dipakai lagi selanjutnya
    // - Membuat fungsi HitObstacle(GameObject) untuk memanggil fungsi menghancurkan obstacle dan menambahkan score pemain
    // - Membuat fungsi DestroyObstacle(GameObject) yang akan menghancurkan obstacle (dengan cara dinonaktifkan)

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

    // Menyimpan apakah obstacle destroyable, di set false secara default untuk problem dimana obstacle masih tidak destroyable. Untuk problem selanjutnya diset true manual pada editor
    [SerializeField] private bool destroyableObstacle = false;

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

        // Objek akan di spawn dengan jumlah random dengan range 3 sampai 10
        int obstacleCount = Random.Range(3, 10);
        Debug.Log("Obstacle Spawned: " + obstacleCount);
        for (int i = 0; i < obstacleCount; i++)
        {
            SpawnRandom();
        }
    }

    // Update is called once per frame
    //void Update(){}

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

    // referensi:
    //- code dari tugas 7 membuat game survival shooter
}
