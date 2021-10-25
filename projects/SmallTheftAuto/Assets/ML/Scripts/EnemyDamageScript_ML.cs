using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript_ML : MonoBehaviour
{
    private int EnemyHealth = 50;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyHealth -= 15; 
            
            if(EnemyHealth <= 0) Destroy(gameObject);    
        }
    }

}
