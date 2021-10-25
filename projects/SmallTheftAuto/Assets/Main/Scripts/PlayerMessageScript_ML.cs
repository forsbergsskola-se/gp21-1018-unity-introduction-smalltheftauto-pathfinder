using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessageScript_ML : MonoBehaviour
{
  
    void Start()
    {
        UIHealthbarScript_ML.OnPlayerDeath += DisplayDeathMessage;
    }

    private void DisplayDeathMessage()
    {
        GetComponentInChildren<Text>().text = "WASTED";
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        GetComponentInChildren<Text>().text = "";
    }
}
