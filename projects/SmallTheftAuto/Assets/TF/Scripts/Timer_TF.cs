using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Timer_TF : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    public float timeStart;
    public TextMeshProUGUI tmTimerText;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        timeStart += Time.deltaTime;
        ShowTimer();
    }

    void ShowTimer()
    {
        string minutes = ((int)timeStart / 60).ToString();
        string seconds = (timeStart % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
        tmTimerText.text = minutes + ":" + seconds;
    }
}
