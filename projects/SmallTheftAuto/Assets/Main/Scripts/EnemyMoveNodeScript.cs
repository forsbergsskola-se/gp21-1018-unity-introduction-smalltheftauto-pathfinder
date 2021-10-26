using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoveNodeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enemy collide");
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collide");
            Vector3 nextPoint = PlayerSpawnerScript_ML.FindClosestsSpawnPoint(transform.position, "EnemyMoveNode");
            other.GetComponent<EnemyMovement>().MoveData = nextPoint;
        }
    }

}
