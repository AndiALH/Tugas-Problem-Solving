using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Notes: script ini akan digunakan pada circle untuk problem 4 dan seterusnya, menggantikan script CircleProb23.cs pada problem 2 dan 3
    // Script versi problem 4
    // addition:
    // - Fungsi KeyboardControl() untuk menggerakkan circle dengan tombol w,a,s,d

    // Tombol untuk control movement circle menggunakan keyboard
    private KeyCode upButton = KeyCode.W;
    private KeyCode downButton = KeyCode.S;
    private KeyCode leftButton = KeyCode.A;
    private KeyCode rightButton = KeyCode.D;

    // Rigidbody 2D dari circle
    private Rigidbody2D rigidBody2D;

    // Kecepatan gerak
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardControl();
    }

    private void KeyboardControl()
    {
        // Fungsi untuk menggerakan circle dengan tombol w,a,s,d.
        // Asumsi sifat movement:
        // - bola bergerak dengan kecepatan konstan dari awal tombol dipencet sampai dilepas
        // - jika tidak ada tombol yang dipencet, maka bola akan langsung berhenti dan diam ditempat
        // - hanya bisa bergerak secara full horizontal (kiri kanan) atau full vertikal (atas bawah). Dengan kata lain, circle tidak bisa bergerak secara diagonal (miring)
        // - jika tombol sedang dipencet, tombol lain dengan arah berlawan tidak bisa dipencet (ex. jika tombol kiri dipencet, tombol kanan tidak berfungsi)

        // Untuk mendapatkan kecepatan circle.
        Vector2 velocity = rigidBody2D.velocity;

        // Jika tombol w ditekan, circle bergerak keatas
        if (Input.GetKey(upButton) && velocity.y >= 0)
        {
            velocity.x = 0.0f;
            velocity.y = speed;
        }

        // Jika tombol s ditekan, circle bergerak kebawah
        else if (Input.GetKey(downButton) && velocity.y <= 0)
        {
            velocity.x = 0.0f;
            velocity.y = -speed;
        }

        // Jika tombol a ditekan, circle bergerak kekiri
        else if (Input.GetKey(leftButton) && velocity.x <= 0)
        {
            velocity.y = 0.0f;
            velocity.x = -speed;
        }

        // Jika tombol a ditekan, circle bergerak kekiri
        else if (Input.GetKey(rightButton) && velocity.x >= 0)
        {
            velocity.y = 0.0f;
            velocity.x = speed;
        }

        // Jika semua tombol tidak ditekan, kecepatan circle jadi nol.
        else
        {
            velocity.x = 0.0f;
            velocity.y = 0.0f;
        }

        // Update velocity rigidBody2D dengan nilai yang telah diupdate.
        rigidBody2D.velocity = velocity;
    }

    // referensi:
    // - code dari tugas 1 membuat game Pong
}
