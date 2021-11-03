using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject DeadCharacter;
    void Start()
    {
        UIHealthbarScript_ML.OnPlayerDeath += PlayerDeath;
    }

    private void PlayerDeath()
    {
        GameObject holdCharacter = Instantiate(DeadCharacter);
        holdCharacter.transform.position = transform.position;
        holdCharacter.transform.rotation = transform.rotation;
        holdCharacter.transform.Rotate(90,0,0);
    }
    
   
}
