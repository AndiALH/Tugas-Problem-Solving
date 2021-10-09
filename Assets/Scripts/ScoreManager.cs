using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Score Manager yang bertugas menampilkan score

    [SerializeField] Text scoreValue;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    void Update()
    {
        // Akan selalu mengupdate score dengan mengecek dari Game Manager
        int currentScore = GameManager.Instance.GetScore();
        scoreValue.text = "" + currentScore;
    }
}
