using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private GameObject wpManager;
    private GameObject[] wps;
    private NavMeshAgent agent;
    
    void Start()
    {
        wps = wpManager.GetComponent<WayPointManager>().waypoints;
        agent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        
    }
}
