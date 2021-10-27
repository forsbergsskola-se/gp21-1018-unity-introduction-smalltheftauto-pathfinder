using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform goal;
    private float speed = 5.0f;
    private float accuracy = 1.0f;
    private float rotSpeed = 2.0f;
    [SerializeField] private GameObject wpManager;
    private GameObject currentNode;
    private GameObject[] wps;  
    private int currentWP = 0;
    private Graph g;
    
    void Start()
    {
        wps = wpManager.GetComponent<WayPointManager>().waypoints;
        g = wpManager.GetComponent<WayPointManager>().graph;
        currentNode = wps[0];
    }

    private void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
        {
            return;
        }

        currentNode = g.getPathPoint(currentWP);

        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position,
            transform.position) < accuracy)
        {
            currentWP++;
        }

        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            
            Vector3 lookAtGoal = new Vector3(goal.position.x, 
                    transform.position.y, goal.position.z);
           
            Vector3 direction = lookAtGoal - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }
    }
}
