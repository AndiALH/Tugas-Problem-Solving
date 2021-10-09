using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Game Manager yang bertugas menyimpan skor dan mengatur event-event lain dalam game
    // Script versi problem 9
    // addition:
    // - Menyimpan score dari enemy selain dari player
    // - Fungsi IncreaseScore ditambahkan parameter string dan dimodifikasi agar bisa digunakan untuk menambahkan score player atau enemy
    // - Menambahkan fungsi untuk mendapatkan score enemy
    // - Menambahkan fungsi StartGame() untuk memulai permainan
    // - Menambahkan time limit untuk permainan
    // - Menambahkan fungsi GetTime() untuk mendapatkan sisa waktu
    // - Menambahkan fungsi GameOver() untuk mengakhiri permainan ketika waktu habis

    // Untuk menyimpan skor
    private static int score;

    // Untuk menyimpan skor enemy
    private static int enemyScore;

    // Menyimpan GameObject player, enemy, dan obstacle manager untuk diaktifkan/dinonaktifkan
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject obstacleManager;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject endPanel;

    // Menyimpan time limit dari game
    [SerializeField] private float timeLimit;

    // Boolean apakah waktu game mulai dihitung
    [SerializeField] private bool startTime;

    // Reference ke control button untuk di click saat game mulai
    [SerializeField] private Button controlButton;

    // Untuk menjadikan object singleton
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartGame("Extreme");
        startTime = false;
        score = 0;
        enemyScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Waktu hanya akan dihitung kalau game sudah mulai
        if (startTime && timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
        }

        // Jika waktu sudah habis, maka akan game over
        if (timeLimit <= 0)
        {
            GameOver();
        }
    }

    public void IncreaseScore(string scorer)
    {
        // Fungsi untuk menambahkan score
        
        // Jika enemy yang mencetak score, maka score enemy yang ditambahkan
        if (scorer == "Enemy")
        {
            enemyScore += 1;
            EnemyCircle.Instance.ResetDelayTimer();
            return;
        }

        // Jika tidak, maka score player yang ditambahkan
        score += 1;
        //Debug.Log("Scored increased to :" + score);
    }

    public int GetScore()
    {
        // Fungsi untuk mendapatkan current score
        return score;
    }

    public float GetTime()
    {
        // Fungsi untuk mendapatkan time remaining
        return timeLimit;
    }

    public int GetEnemyScore()
    {
        // Fungsi untuk mendapatkan current score
        return enemyScore;
    }

    public void StartGame(string difficulty, string control)
    {
        // Fungsi ini dipanggil untuk memulai permainan setelah selection screen. Hanya dipakai di problem 9

        // Mengaktifkan object-object yang di disable
        if (player != null && enemy != null && obstacleManager != null && enemy != null && startPanel != null)
        {
            player.SetActive(true);
            enemy.SetActive(true);
            obstacleManager.SetActive(true);
            startPanel.SetActive(false);
        }

        // Set difficulty sesuai yang dipilih
        EnemyCircle.Instance.SetDifficulty(difficulty);

        // Set control sesuai yang dipilih
        if (control == "Mouse")
        {
            controlButton.onClick.Invoke();
        }

        startTime = true;
    }

    private void GameOver()
    {
        // Fungsi jika waktu telah berakhir maka game akan selesai

        if (player != null && enemy != null && obstacleManager != null && enemy != null && endPanel != null)
        {
            player.SetActive(false);
            enemy.SetActive(false);
            obstacleManager.SetActive(false);
            endPanel.SetActive(true);
        }
    }

}
