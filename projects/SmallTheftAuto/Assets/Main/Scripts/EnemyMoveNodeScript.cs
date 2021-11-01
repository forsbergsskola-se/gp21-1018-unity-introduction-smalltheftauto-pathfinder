using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoveNodeScript : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 nextPoint = PlayerSpawnerScript_ML.FindClosestsSpawnPoint(transform.position, "EnemyMoveNode");
            other.GetComponent<NPCMovement>().MoveData = nextPoint;
        }
    }

}
