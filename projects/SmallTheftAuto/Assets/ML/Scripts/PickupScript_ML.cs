using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupTypes
{
    Handgun,
    Machinegun,
    Health
}

public class PickupScript_ML : MonoBehaviour
{
    private PickupTypes pickupTypes;
    public delegate void PickupEventHandler(PickupTypes pickupType);
    public static event PickupEventHandler PickupPicked;
    
    
    
    protected virtual void OnPickupPicked(PickupTypes pickupType)
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
            OnPickupPicked(PickupTypes.Handgun);
        }
        else if (CompareTag("Machinegun"))
        {
            OnPickupPicked(PickupTypes.Machinegun);
        }
        else if (CompareTag("Health"))
        {
            OnPickupPicked(PickupTypes.Health);
        }
        
        Destroy(gameObject);
    }

}
