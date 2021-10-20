using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript_ML : MonoBehaviour
{
    public bool fire = false;
    public Vector3 forward;
    private Vector3 originalPos;
    void Start()
    {
        originalPos = transform.position;
    }


    void Update()
    {
        if (fire)
        {
            transform.position += forward * 7.0f * Time.deltaTime;

            if (Vector3.Distance(originalPos, transform.position) > 50)
            {
                Destroy(gameObject);
            }
        }
    }
}
