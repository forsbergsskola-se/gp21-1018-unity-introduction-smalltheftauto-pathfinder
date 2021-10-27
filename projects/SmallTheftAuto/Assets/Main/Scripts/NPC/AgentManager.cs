using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    private GameObject[] agents;
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Enemy");
    }

    
    void Update()
    {
        
    }
}
