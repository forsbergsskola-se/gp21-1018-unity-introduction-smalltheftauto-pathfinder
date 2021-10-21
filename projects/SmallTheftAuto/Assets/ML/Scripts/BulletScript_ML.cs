using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript_ML : MonoBehaviour
{
    public bool fire = false;
    private Vector3 originalPos;
    private Vector3 originalForward;
    private BoxCollider collider;
    
    void Start()
    {
        originalPos = transform.position;
        originalForward = Movement_ML.PlayerForward;
        collider = GetComponent<BoxCollider>();
        collider.enabled = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bullet hit");
        Destroy(gameObject);
    }




    void Update()
    {
        if (fire)
        {
            transform.position += originalForward * 3.0f * Time.deltaTime;

            if (Vector3.Distance(originalPos, transform.position) > 30)
            {
                Destroy(gameObject);
            }
            
            
            
        }
    }
}
