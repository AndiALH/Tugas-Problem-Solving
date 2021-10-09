using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Notes: script ini akan digunakan pada circle untuk problem 4 dan seterusnya, menggantikan script CircleProb23.cs pada problem 2 dan 3
    // Script versi problem 7
    // addition:
    // - Menambahkan fungsi OnTriggerEnter2D yang akan mendeteksi ketika circle collide dengan obstacle

    //Untuk menjadikan object singleton
    private static Circle _instance = null;
    public static Circle Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Circle>();
            }

            return _instance;
        }
    }

    // Tombol untuk control movement circle menggunakan keyboard
    private KeyCode upButton = KeyCode.W;
    private KeyCode downButton = KeyCode.S;
    private KeyCode leftButton = KeyCode.A;
    private KeyCode rightButton = KeyCode.D;

    // Rigidbody 2D dari circle
    private Rigidbody2D rigidBody2D;

    // Kecepatan gerak
    public float speed = 5.0f;

    // Value untuk menentukan kontrol apa yang sedang dipakai. Default yang dipakai adalah keyboard
    private bool keyboard_toggle = true;
    private bool mouse_toggle = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Default control yang dipakai adalah keyboard, di set dari problem 5 agar saat semua problem digabungkan tidak perlu diatur-atur lagi
        // keyboard_toggle = true;
        // mouse_toggle = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (keyboard_toggle)
            KeyboardControl();

        //mouse_toggle = true;
        if (mouse_toggle)
            MouseControl();
    }

    private void KeyboardControl()
    {
        // Fungsi untuk menggerakan circle dengan tombol w,a,s,d.
        // Notes sifat movement:
        // - circle bergerak dengan kecepatan konstan dari awal tombol dipencet sampai dilepas
        // - jika tidak ada tombol yang dipencet, maka bola akan langsung berhenti dan diam ditempat
        // - hanya bisa bergerak secara full horizontal (kiri kanan) atau full vertikal (atas bawah). Dengan kata lain, circle tidak bisa bergerak secara diagonal (miring)
        // - jika tombol sedang dipencet, tombol lain dengan arah berlawan tidak bisa dipencet (ex. jika tombol kiri dipencet, tombol kanan tidak berfungsi)

        // Untuk mendapatkan kecepatan circle
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

    private void MouseControl()
    {
        // Fungsi untuk menggerakan circle dengan posisi mouse pada screen game.
        // Notes sifat movement:
        // - asumsi circle selalu bergerak mengikuti posisi mouse di layar, bukan hanya saat layar di klik
        // - circle bergerak dengan kecepatan berubah sesuai dengan jarak circle dengan mouse, namun dibatasi dengan kecepatan maksimal yang dapat dicapai
        // - sengaja tidak menggunakan metode Lerp atau MoveTowards, agar sifat circle yang memantul pada dinding tetap ada dan berfungsi

        // Untuk mendapatkan posisi mouse
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Untuk mendapatkan kecepatan x dan y berdasarkan selisih posisi mouse dan posisi object. Dikali
        float mouse_x = (mousePos.x - transform.position.x) * 3;
        float mouse_y = (mousePos.y - transform.position.y) * 3;

        // Untuk mendapatkan kecepatan circle
        Vector2 velocity = rigidBody2D.velocity;

        // Mengubah kecepatan x dan y circle dengan nilai yang didapatkan sebelumnya, namun dengan batas maksimum.
        velocity.x = GetMaxSpeed(mouse_x);
        velocity.y = GetMaxSpeed(mouse_y);
        rigidBody2D.velocity = velocity;
    }

    private float GetMaxSpeed(float mouse_speed)
    {
        // Fungsi yang akan mengembalikan kecepatan maksimal dengan batas speed yang telah ditentukan.

        //Jika speed positif, maka akan dikembalikan kecepatan minimum dengan batas positif
        if (mouse_speed >= 0)
        {
            return Mathf.Min(mouse_speed, speed);
        }
        //Jika speed negatif, maka akan dikembalikan kecepatan maksimum (karena dalam negatif jadi terbalik) dengan batas negatif
        else
        {
            return Mathf.Max(mouse_speed, -speed);
        }
    }

    public string ChangeControlInput()
    {
        // Metode untuk mengganti input controller dari keyboard ke mouse atau sebaliknya
        keyboard_toggle = !keyboard_toggle;
        mouse_toggle = !mouse_toggle;

        // String untuk digunakan pada text di tombol
        Debug.Log("Control Changed");
        return (keyboard_toggle ? "Keyboard" : "Mouse");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Metode untuk mendeteksi jika collide dengan trigger dari obstacle. Jika collide, maka akan memanggil fungsi yang akan menghilangkan obstacle dan menambahkan skor

        if (collision.tag == "Obstacle")
        {
            //Debug.Log("Collide with obstacle");

            // Akan memanggil metode pada obstacle manager yang akan melakukan proses lebih lanjut
            ObstacleManager.Instance.HitObstacle(collision.gameObject);
        }
    }

    // referensi:
    // - code dari tugas 1 membuat game Pong
    // - https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/#mouse_position_world_2D
    // - code dari tugas 8 membuat incremental game
}
