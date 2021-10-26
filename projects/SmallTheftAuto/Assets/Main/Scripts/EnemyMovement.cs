using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyMoves
{
    Stopping,
    Moving
}

public class EnemyMovement : MonoBehaviour
{
    public Vector3 MoveData { get; set; }
    private NavMeshAgent agent;
    private Vector3 nextPoint;
    private bool ready = true;
    private Dictionary<int, Vector3> movePoints;
    private int counter = 0;
    public Vector3 target;
    private NavMeshPath path;
    private float elapsed = 0.0f;
    private bool flipFlop = true;
    private int pointIndex = 0;

    void Start()
    {
        movePoints = new Dictionary<int, Vector3>(4);
        SetMovePoints();
        agent = GetComponent<NavMeshAgent>(); 
        FindNewPoint();

        if (agent)
        {
            Debug.Log("agent exists");    
        }
        path = new NavMeshPath();
    }
        
    
    
    private void SetMovePoints()
    {
        movePoints.Add(0,transform.position + new Vector3(0, 0, 10));
        movePoints.Add(1, movePoints[0] - new Vector3(0, 0, 10));
    }
    
    private void FindNewPoint()
    {
        
        
        if (movePoints.ContainsKey(pointIndex))
        {
            MoveData = PlayerSpawnerScript_ML.FindClosestsSpawnPoint(transform.position, "EnemyMoveNode");
            agent.destination = movePoints[pointIndex];
            pointIndex++;
        }
    }
    
    
    void Update()
    {
        
        if(agent.remainingDistance < 0.1f)
        {
            FindNewPoint();
        }
    
    }
}
