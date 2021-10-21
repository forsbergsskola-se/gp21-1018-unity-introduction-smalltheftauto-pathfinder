using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript_ML : MonoBehaviour
{
    
    public delegate void PickupEventHandler(string pickupType);
    public static event PickupEventHandler PickupPicked;

    protected virtual void OnPickupPicked(string pickupType)
    {
        if (PickupPicked != null)
        {
            PickupPicked(pickupType);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (CompareTag("Handgun"))
        {
            OnPickupPicked("Handgun");
        }
        else if (CompareTag("Machinegun"))
        {
            
        }
        else if (CompareTag("Health"))
        {
            
        }
        
        Destroy(gameObject);
    }

}
