using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    // Script untuk mengupdate time di UI

    [SerializeField] Text timeValue;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    void Update()
    {
        // Akan selalu mengupdate score dengan mengecek dari Game Manager
        float timeRemaining = GameManager.Instance.GetTime();
        timeValue.text = "" + Mathf.Ceil(timeRemaining);
    }
}
