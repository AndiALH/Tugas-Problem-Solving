using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPanel : MonoBehaviour
{
    // Fungsi problem 10 untuk panel pemilihan problem

    [SerializeField] private Button problem1Button;
    [SerializeField] private Button problem2Button;
    [SerializeField] private Button problem3Button;
    [SerializeField] private Button problem4Button;
    [SerializeField] private Button problem5Button;
    [SerializeField] private Button problem6Button;
    [SerializeField] private Button problem7Button;
    [SerializeField] private Button problem8Button;
    [SerializeField] private Button problem9Button;
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        problem1Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 1
            SceneManager.LoadScene(1);
        });

        problem2Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 2
            SceneManager.LoadScene(2);
        });

        problem3Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 3
            SceneManager.LoadScene(3);
        });

        problem4Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 4
            SceneManager.LoadScene(4);
        });

        problem5Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 5
            SceneManager.LoadScene(5);
        });

        problem6Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 6
            SceneManager.LoadScene(6);
        });

        problem7Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 7
            SceneManager.LoadScene(7);
        });

        problem8Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 8
            SceneManager.LoadScene(8);
        });

        problem9Button.onClick.AddListener(() =>
        {
            // Memanggil method untuk masuk ke problem 9
            SceneManager.LoadScene(9);
        });

        exitButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk keluar dari game
            Application.Quit();
        });
    }
}
