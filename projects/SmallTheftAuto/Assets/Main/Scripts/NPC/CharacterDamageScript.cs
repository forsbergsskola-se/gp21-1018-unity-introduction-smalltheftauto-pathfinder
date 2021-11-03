using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageScript : MonoBehaviour
{
    private int EnemyHealth = 50;
    [SerializeField] private GameObject DeadCharacter;
    private GameObject holdCharacter;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyHealth -= 15;

            if (EnemyHealth <= 0)
            {
                SpawnDeadCharacter();
            }
        }
    }

    private void SpawnDeadCharacter()
    {
        holdCharacter = Instantiate(DeadCharacter);
        holdCharacter.transform.position = transform.position;
        holdCharacter.transform.rotation = transform.rotation;
        holdCharacter.transform.Rotate(90,0,0);
        
        Destroy(gameObject);
    }

}
