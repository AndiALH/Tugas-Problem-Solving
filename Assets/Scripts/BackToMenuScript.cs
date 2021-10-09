using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenuScript : MonoBehaviour
{
    // Fungsi untuk tombol back to menu

    [SerializeField] private Button backToMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        backToMenuButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 1
            SceneManager.LoadScene(0);
        });
    }
}
