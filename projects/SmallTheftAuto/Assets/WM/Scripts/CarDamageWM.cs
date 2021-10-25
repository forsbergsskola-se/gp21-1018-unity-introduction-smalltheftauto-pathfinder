using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamageWM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Untagged")
        {
            other.gameObject.GetComponent<CarHealthWM>().CarDamage(10);
        }
    }
}
