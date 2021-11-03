using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class PlayerCharacterEventHandler_ML : MonoBehaviour
{
    private bool readyToSpawn = false;
    private bool spawning = false;
    
    void Start()
    {
        UIHealthbarScript_ML.OnPlayerDeath += PlayerDies;
    }

    private void PlayerDies()
    {
        spawning = true;
    }

    

    private IEnumerator DelaySpawn()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        readyToSpawn = true;
    }
}
