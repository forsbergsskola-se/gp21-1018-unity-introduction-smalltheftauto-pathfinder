using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer_TF : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    private int counter = 10;
    private float timeStart;
    public TextMeshProUGUI tmTimerText;
    public bool isFinnished = false;

    
    void Start()
    {
        StartCoroutine(countDownToRestart());
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

        tmTimerText.text = $"{minutes}:{seconds}";
    }

    IEnumerator countDownToRestart()
    {
        while (counter >= 0)
        {
            timerText.text = counter.ToString();
            yield return new WaitForSeconds(1f);
            counter--;
        }

        Reset();

    }

    private void Reset()
    {
        SceneManager.LoadScene("TF_Game");
    }
}
