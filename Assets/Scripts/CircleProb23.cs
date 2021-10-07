using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleProb23 : MonoBehaviour
{
    // NOTES: Script ini hanya digunakan untuk membuat lingkaran bergerak dengan kecepatan konstan, dan hanya akan digunakan untuk problem 2 dan 3

    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        //Diberi jeda 3 detik karena saat dimainkan di build biasanya bola sudah hilang sebelum loading selesai
        Invoke("PushBall", 3);
    }

    private void PushBall()
    {
        Vector2 force = new Vector2(100, 100);
        rigidBody2D.AddForce(force);
        Debug.Log("ball pushed");
    }

    // referensi: code dari tugas 1 membuat game Pong
}
