using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    // Script tombol untuk mengganti control input dari keyboard ke mouse atau sebaliknya

    [SerializeField] private Button controlButton;
    [SerializeField] private Text controlButtonText;
        
    // Start is called before the first frame update
    void Start()
    {
        controlButton.onClick.AddListener(() =>
        {
            // Memanggil method untuk mengganti input control pada class Circle
            string current_input = Circle.Instance.ChangeControlInput();
            controlButtonText.text = current_input;
        });
    }
}
