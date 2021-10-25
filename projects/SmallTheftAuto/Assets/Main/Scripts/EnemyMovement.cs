using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 nextPoint;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextPoint = FindNewPoint();

    }

    private Vector3 FindNewPoint()
    {
        return transform.position + new Vector3(200, 0, 0);
    }
    
    
    void Update()
    {
        agent.destination = nextPoint;
    }
}
