using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Script versi problem 9
    // addition:
    // - Ditambahkan update untuk score enemy

    [SerializeField] Text scoreValue;
    [SerializeField] Text enemyScoreValue;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    void Update()
    {
        // Akan selalu mengupdate score dengan mengecek dari Game Manager
        int currentScore = GameManager.Instance.GetScore();
        scoreValue.text = "" + currentScore;

        if (enemyScoreValue != null)
        {
            int enemyScore = GameManager.Instance.GetEnemyScore();
            enemyScoreValue.text = "" + enemyScore;
        }
        
    }
}
