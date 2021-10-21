using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    private int EnemyHealth = 50;
    void Start()
    {
       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyHealth -= 15; 
            
            
            if(EnemyHealth <= 0) Destroy(gameObject);    
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
