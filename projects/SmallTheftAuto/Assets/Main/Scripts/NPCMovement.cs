using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyMoves
{
    Stopping,
    ReadyToMove,
    Moving
}

public class NPCMovement : MonoBehaviour
{
    private EnemyMoves _enemyMoves;
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
    [SerializeField] private float turnSpeed = 7;
    private Quaternion lookAtThis;

    void Start()
    {
        movePoints = new Dictionary<int, Vector3>(4);
        SetMoveNodes();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = FindNearestPoint();

        path = new NavMeshPath();
    }

    private void SetMoveNodes()
    {
        int count = 0;
        foreach (var el in GameObject.FindGameObjectsWithTag("EnemyMoveNode"))
        {
            movePoints.Add(count, el.transform.position);
            count++;
        }
    }

    private Vector3 FindNearestPoint()
    {
        Vector3 point = movePoints[0];
        float distance = 0;
        float currentMin = Vector3.Distance(transform.position, movePoints[0]);
        foreach (var el in movePoints)
        {
            distance = Vector3.Distance(transform.position, el.Value);

            if (distance < currentMin && distance > 1)
            {
                point = el.Value;
                currentMin = distance;
            }
        }

        return point;
    }
    
    private void SetMovePoints()
    {
        movePoints.Add(0,transform.position + new Vector3(0, 0, 10));
        movePoints.Add(1, movePoints[0] - new Vector3(0, 0, 10));
    }

    private void FindNextPoint()
    {
        
    }
    
    private void SimplePatrol()
    {
        if (flipFlop)
        {
            agent.destination = movePoints[0];
            flipFlop = false;
        }
        else
        {
            agent.destination = movePoints[1];
            flipFlop = true;
        }
    }
    
    
    void Update()
    {
        
        if(agent.remainingDistance < 0.1f)
        {
            StartCoroutine(DelayMove(0.5f));  
        //    agent.destination = movePoints[counter];
            counter++;
            if (counter >= movePoints.Count)
            {
                counter = 0;
            }
        }
    
    }

    private void TakeALook()
    {
        lookAtThis = Quaternion.LookRotation(movePoints[counter] - transform.position);
    }

    private IEnumerator DelayMove(float delayTime)
    {
        TakeALook();
        yield return new WaitForSeconds(delayTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtThis, Time.deltaTime * turnSpeed);
        agent.destination = movePoints[counter];
        
    }
}
