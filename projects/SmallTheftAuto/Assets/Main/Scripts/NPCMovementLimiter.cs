using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCMovementLimiter : MonoBehaviour
{
    // Start is called before the first frame update

    private Rect rect;
    void Start()
    {
        rect = new Rect();

        rect.size = new Vector2(50, 50);
        
        
    }

    private void OnDrawGizmos()
    {
    //    Gizmos.color = Color.green;
        
    //    Gizmos.DrawWireCube(transform.position, new Vector3(20,20,20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
