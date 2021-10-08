using UnityEngine;

public interface IFactory
{
    // Interface untuk Factory pattern
    // Script versi problem 6
    // Interface methods CreateObject(int, Vector3) dan CreateRandomObject(Vector3)

    GameObject CreateObject(int tag, Vector3 spawn_pos);

    GameObject CreateRandomObject(Vector3 spawn_pos);

    // referensi:
    //- code dari tugas 7 membuat game survival shooter
}
