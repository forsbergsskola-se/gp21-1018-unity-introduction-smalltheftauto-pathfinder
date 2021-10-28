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
    public EnemyMoves _enemyMoves;
    
    public Vector3 MoveData { get; set; }
    private NavMeshAgent agent;
    private Vector3 nextPoint;
    private bool ready = true;
    private Dictionary<int, Vector3> movePoints;
    private int counter = 0;
    public Vector3 target;
    private NavMeshPath path;
    private float elapsed = 0.0f;

    private int pointIndex = 0;
    [SerializeField] private float turnSpeed = 7;
    private Quaternion lookAtThis;
    public Transform relevantTransform;

    void Start()
    {
        movePoints = new Dictionary<int, Vector3>(4);
        agent = GetComponent<NavMeshAgent>();
        
        SetMoveNodes(45);
        SortMoveNodes();
        counter++;
        
        path = new NavMeshPath();
    }

    private void SetMoveNodes(float maxNodeDistance)
    {
        int count = 0;
        foreach (var el in GameObject.FindGameObjectsWithTag("EnemyMoveNode"))
        {
            if (Vector3.Distance(transform.position, el.transform.position) <= maxNodeDistance)
            {
                movePoints.Add(count, el.transform.position);
                count++;
            }
        }
    }

    private void SortMoveNodes()
    {
        Dictionary<int, Vector3> tempDict = new Dictionary<int, Vector3>(movePoints.Count);

        
        for (int i = 0; i < movePoints.Count; i++)
        {
            var input = movePoints[i];
            var item = Vector3.Distance(transform.position, movePoints[i]);
            var currentIndex = i;
            
            
            while (currentIndex > 0 && Vector3.Distance(transform.position, tempDict[i - 1]) > item)
            {
                tempDict[currentIndex] = tempDict[currentIndex - 1];
                currentIndex--;
            }
            
            tempDict.Remove(currentIndex);
            tempDict.Add(currentIndex, input);
        }

        movePoints = tempDict;
        
        foreach (var el in tempDict)
        {
            var item = Vector3.Distance(transform.position, el.Value);
            Debug.Log(el.Key +  " " + item);
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

 
    public void SetADestination(Vector3 destination)
    {
        agent.destination = destination;
    }
    
    

    void Update()
    {

        if (Vector3.Distance(agent.destination, transform.position) < 3)
        {
            agent.destination = transform.position;
        }
    }


    private IEnumerator DelayMove(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtThis, Time.deltaTime * turnSpeed);
        agent.destination = movePoints[counter];
        
    }
}
