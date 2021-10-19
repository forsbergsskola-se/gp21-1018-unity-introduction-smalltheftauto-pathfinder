using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject thisPickup;
    
    
    void Start()
    {
        thisPickup = GameObject.FindWithTag("Pickup");    
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (thisPickup != null)
        {
            Destroy(thisPickup);
        }

        Debug.LogFormat("Object {0} entered trigger {1}!",
            other.name, this.name);
    }

}
