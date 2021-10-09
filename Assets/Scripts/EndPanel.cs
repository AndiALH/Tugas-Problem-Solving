using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    // Script untuk end panel

    [SerializeField] private Button restartButton;
    [SerializeField] private Text winLoseText;

    // Start is called before the first frame update
    void Start() 
    {
        // Mengecek apakah pemain menang atau kalah
        int playerScore = GameManager.Instance.GetScore();
        int enemyScore = GameManager.Instance.GetEnemyScore();
        if (playerScore > enemyScore)
        {
            winLoseText.text = "Player Win";
        }
        if (playerScore < enemyScore)
        {
            winLoseText.text = "Bot Win";
        }

        restartButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk restart game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    // Update is called once per frame
    //void Update(){}
}
