using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Animator anim = new Animator();
    private Transform player;
    private State currentState;
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    //    anim = GetComponent<Animator>()
        currentState = new Idle(gameObject, Agent, anim, player);
    }
    
    void Update()
    {
        currentState = currentState.Process();
    }
}
