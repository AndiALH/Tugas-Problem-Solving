using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    // Script untuk start panel

    [SerializeField] private Button easyButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Button extremeButton;
    [SerializeField] private Button keyboardButton;
    [SerializeField] private Button mouseButton;

    [SerializeField] private Text easyButtonText;
    [SerializeField] private Text hardButtonText;
    [SerializeField] private Text extremeButtonText;
    [SerializeField] private Text keyboardButtonText;
    [SerializeField] private Text mouseButtonText;

    [SerializeField] private Button startButton;

    private string selectedDifficulty;
    private string selectedControl;

    // Start is called before the first frame update
    void Start()
    {
        selectedDifficulty = "Easy";
        selectedControl = "Keyboard";
        easyButtonText.color = Color.blue;
        keyboardButtonText.color = Color.blue;

        easyButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk mengganti difficulty menjadi easy
            selectedDifficulty = "Easy";
            easyButtonText.color = Color.blue;
            hardButtonText.color = Color.black;
            extremeButtonText.color = Color.black;
        });

        hardButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk mengganti difficulty menjadi hard
            selectedDifficulty = "Hard";
            easyButtonText.color = Color.black;
            hardButtonText.color = Color.blue;
            extremeButtonText.color = Color.black;
        });

        extremeButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk mengganti difficulty menjadi extreme
            selectedDifficulty = "Extreme";
            easyButtonText.color = Color.black;
            hardButtonText.color = Color.black;
            extremeButtonText.color = Color.blue;
        });

        keyboardButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk mengganti control menjadi keyboard
            selectedControl = "Keyboard";
            keyboardButtonText.color = Color.blue;
            mouseButtonText.color = Color.black;
        });

        mouseButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk mengganti control menjadi mouse
            selectedControl = "Mouse";
            keyboardButtonText.color = Color.black;
            mouseButtonText.color = Color.blue;
        });

        startButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk memulai game sesuai difficulty dan control yang dipilih
            Debug.Log("Start Game dengan difficulty " + selectedDifficulty + " dan control " + selectedControl);
            GameManager.Instance.StartGame(selectedDifficulty, selectedControl);
        });
    }

    // Update is called once per frame
    //void Update() {}
}
