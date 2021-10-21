using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer_TF : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    public float timeStart;

    
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
    }
}
