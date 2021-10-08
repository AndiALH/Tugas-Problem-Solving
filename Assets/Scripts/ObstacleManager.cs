using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //Manager obstacle yang bertugas melakukan spawn obstacle
    // Script versi problem 6
    // addition:
    // - metode SpawnRandom yang akan melakukan spawn obstacle dengan posisi random

    // Menyimpan batas-batas area game
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    [SerializeField] private Transform topBorder;
    [SerializeField] private Transform bottomBorder;

    //Untuk menyimpan batas area spawn
    private float min_x;
    private float min_y;
    private float max_x;
    private float max_y;

    private float area_padding = 0.4f;

    // Untuk factory pattern
    [SerializeField]
    MonoBehaviour obstacleFactory;
    IFactory Factory { get { return obstacleFactory as IFactory; } }

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 spawn_pos = new Vector3
        {
            x = Random.Range(min_x * 100, max_x * 100) / 100,
            y = Random.Range(min_y * 100, max_y * 100) / 100,
            z = 0
        };

        // Metode untuk mengecek apakah ada collider lain dalam area spawn, diperiksa sehingga waktu object di spawn tidak tabrakan
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawn_pos, 0.3f);

        // Jika area spawn collide dengan object lain, maka object akan di spawn ulang
        if (colliders.Length > 0)
        {
            SpawnRandom();
            return;
        }

        // Memanggil metode untuk membuat obstacle dan menyimpan obstacle di variabel
        GameObject obstacle = Factory.CreateRandomObject(spawn_pos);
    }

    // referensi:
    //- code dari tugas 7 membuat game survival shooter
}
