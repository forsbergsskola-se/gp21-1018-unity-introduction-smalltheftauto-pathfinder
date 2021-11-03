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
        SaveSystem.OnGatherSaveData += DisplaySaveMessage;
    }

    private void DisplaySaveMessage()
    {
        GetComponentInChildren<Text>().text = "GAME SAVED";
        StartCoroutine(Delay());
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
