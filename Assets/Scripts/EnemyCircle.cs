using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour
{

    // Rigidbody 2D dari circle
    private Rigidbody2D rigidBody2D;

    // Tansform target yang sedang diincar
    private Vector3 currentTarget;

    //Untuk menjadikan object singleton
    private static EnemyCircle _instance = null;
    public static EnemyCircle Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyCircle>();
            }

            return _instance;
        }
    }

    // Enemy delay untuk setiap difficulty
    [SerializeField] private float delayEasy = 0.7f;
    [SerializeField] private float delayHard = 0.4f;
    [SerializeField] private float delayExtreme = 0.1f;
    // Untuk menyimpan delay time yang dipilih
    private float enemyDelay;
    // Untuk menyimpan timer untuk delay
    private float delayRemaining;

    // Kecepatan gerak
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentTarget = transform.position;
        //enemyDelay = 0;
        //delayRemaining = enemyDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Mengecek distance ke target
        float distance = GetDistanceToTarget();

        // Jika sedang diam dan distance ke target 0, maka akan dicari target terdekat baru
        if (distance <= 0)
        {
            FindNearbyTarget();
        }
    }

    void FixedUpdate()
    {
        // Mengecek distance ke target
        float distance = GetDistanceToTarget();

        // Akan bergerak ke posisi target jika jarak target lebih dari 0
        if (distance > 0)
        {
            MoveToTarget();
        }
    }

    private float GetDistanceToTarget()
    {
        // Fungsi untuk mendapatkan distance ke target

        return Vector3.Distance(transform.position, currentTarget);
    }

    private void FindNearbyTarget()
    {
        // Fungsi untuk mendapatkan target terdekat

        // Mendapatkan list obstacle yang ada
        List<GameObject> targetList = ObstacleManager.Instance.spawnedObstacle;
        //Debug.Log("" + targetList.Count);

        if (targetList.Count == 0)
        {
            return;
        }

        // Untuk mencari target yang paling dekat dari list obstacle dengan melihat distance squared, karena lebih efisien(?) dari distance biasa.
        // Source dari code dituliskan di referensi dibawah
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject target in targetList)
        {
            Transform targetTrans = target.transform;
            Vector3 directionToTarget = targetTrans.position - currentPosition;
            float directionSquare = directionToTarget.sqrMagnitude;
            if (directionSquare < closestDistanceSqr)
            {
                closestDistanceSqr = directionSquare;
                bestTarget = targetTrans;
            }
        }
        currentTarget = bestTarget.position; 
        //Debug.Log("best target position : " + bestTarget.position.x + " " + bestTarget.position.y + " " + bestTarget.position.z + " ");
    }

    private void MoveToTarget()
    {
        // Fungsi untuk bergerak ke posisi target

        // Untuk mengurangi delay. Jika delay belum mencapai 0, maka tidak akan dilakukan movement
        delayRemaining -= Time.deltaTime;
        if (delayRemaining > 0)
            return;

        //idle = false;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Metode untuk mendeteksi jika collide dengan trigger dari obstacle. Jika collide, maka akan memanggil fungsi yang akan menghilangkan obstacle dan menambahkan skor

        if (collision.tag == "Obstacle")
        {
            //Debug.Log("Collide with obstacle");

            // Akan memanggil metode pada obstacle manager yang akan melakukan proses lebih lanjut
            ResetTarget();
            ObstacleManager.Instance.HitObstacle(collision.gameObject, "Enemy");
        }
    }

    public void ResetTarget()
    {
        // Fungsi untuk me reset target enemy sehingga enemy akan mencari target baru

        //Debug.Log("Target reset");
        currentTarget = transform.position;
    }

    public void ResetDelayTimer()
    {
        // Fungsi untuk mereset timer delay. Delay time hanya akan di reset ketika enemy mendapatkan score
        delayRemaining = enemyDelay;
    }

    public void SetDifficulty(string difficulty)
    {
        // Fungsi untuk menentukan difficulty dari enemy

        if (difficulty == "Easy")
        {
            enemyDelay = delayEasy;
        }

        if (difficulty == "Hard")
        {
            enemyDelay = delayHard;
        }

        if (difficulty == "Extreme")
        {
            enemyDelay = delayExtreme;
        }

        delayRemaining = enemyDelay;
    }

    // referensi:
    //- https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
}
