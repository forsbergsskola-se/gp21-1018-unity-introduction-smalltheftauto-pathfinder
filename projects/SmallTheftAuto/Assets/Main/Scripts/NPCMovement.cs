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
    private GameObject tracker;
    public Transform relevantTransform;

    void Start()
    {
        movePoints = new Dictionary<int, Vector3>(4);
        agent = GetComponent<NavMeshAgent>();
        
        SetMoveNodes(45);
        SortMoveNodes();
        agent.destination = movePoints[0];
        counter++;
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;


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

    public void EnemySeen()
    { 
        lookAtThis = Quaternion.LookRotation(relevantTransform.position - transform.position);
        lookAtThis.x = 0;
        lookAtThis.z = 0;
        transform.rotation = lookAtThis;
    }
    
    private void ProgressTracker()
    {
   //     transform.rotation = Quaternion.Slerp(transform.rotation, lookAtThis, Time.deltaTime * turnSpeed);
   //    transform.position += Vector3.forward * Time.deltaTime * 3;

        if (Vector3.Distance(tracker.transform.position, movePoints[counter]) < 1)
        {
            counter++;
            if (counter >= movePoints.Count)
            {
                counter = 0;
            }
        }
   
        if(agent.remainingDistance < 0.1f)
        {
        //    StartCoroutine(DelayMove(0.5f));  
        //    agent.destination = movePoints[counter];
            
            counter++;
            if (counter >= movePoints.Count)
            {
                counter = 0;
            }
        }
        
        tracker.transform.LookAt(movePoints[counter]);
        tracker.transform.Translate(0,0,6 * Time.deltaTime);
    }


    void Update()
    {
       
    //    ProgressTracker();
    }

    private void TakeALook()
    {
    //    lookAtThis = Quaternion.LookRotation(movePoints[counter] - transform.position);
    }

    private IEnumerator DelayMove(float delayTime)
    {
        TakeALook();
        yield return new WaitForSeconds(delayTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtThis, Time.deltaTime * turnSpeed);
        agent.destination = movePoints[counter];
        
    }
}
