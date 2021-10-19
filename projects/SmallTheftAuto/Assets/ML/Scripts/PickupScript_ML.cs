using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript_ML : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject thisPickup;
    private Component pickupType;

    public delegate void PickupEventHandler();
    public static event PickupEventHandler PickupPicked;
    
    void Start()
    {
        
    }

    protected virtual void OnPickupPicked()
    {
        if (PickupPicked != null)
        {
            PickupPicked();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (CompareTag("Handgun"))
        {
            OnPickupPicked();
        }
        else if (CompareTag("Machinegun"))
        {
            
        }
        
        
   
        Destroy(gameObject);
      

    }

}
