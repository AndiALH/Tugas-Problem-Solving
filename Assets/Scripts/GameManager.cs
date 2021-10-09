using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game Manager yang bertugas menyimpan skor dan mengatur event-event lain dalam game
    // Script versi problem 7
    // addition:
    // - Membuat script singleton
    // - Membuat fungsi IncreaseScore() untuk menambahkan score
    // - Membuat fungsi GetScore() untuk mendapatkan score

    //untuk menyimpan skor
    private static int score = 0;

    //Untuk menjadikan object singleton
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
    //void Start(){}

    // Update is called once per frame
    //void Update(){}

    public void IncreaseScore()
    {
        // Fungsi untuk menambahkan score
        score+=1;
        Debug.Log("Scored increased to :" + score);
    }

    public int GetScore()
    {
        // Fungsi untuk mendapatkan current score
        return score;
    }
}
