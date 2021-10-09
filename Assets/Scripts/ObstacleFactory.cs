using UnityEngine;

public class ObstacleFactory : MonoBehaviour, IFactory
{
    // Class factory untuk membuat obstacle
    // Script versi problem 7
    // addition:
    // - memperbaiki index random yang diambil untuk tag object, karena sebelumnya ada satu prefabs yang tidak akan terambil sebagai obstacle yang di respawn

    //Array dari semua prefab obstacle
    [SerializeField] private GameObject[] obstaclePrefabs;

    // Start is called before the first frame update
    //void Start(){}

    public GameObject CreateObject(int tag, Vector3 spawn_pos)
    {
        // Fungsi untuk membuat obstacle

        // Jika tag lebih dari index terakhir, maka akan diambil index terakhir saja
        tag = Mathf.Min(tag, obstaclePrefabs.Length-1);

        GameObject obstacle = Instantiate(obstaclePrefabs[tag], spawn_pos, Quaternion.identity);
        return obstacle;
    }

    public GameObject CreateRandomObject(Vector3 spawn_pos)
    {
        // Fungsi untuk membuat obstacle secara random

        // Untuk mengambil obstacle random yang ada pada array prefab
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = CreateObject(index, spawn_pos);
        return obstacle;
    }

    // referensi:
    //- code dari tugas 7 membuat game survival shooter
}