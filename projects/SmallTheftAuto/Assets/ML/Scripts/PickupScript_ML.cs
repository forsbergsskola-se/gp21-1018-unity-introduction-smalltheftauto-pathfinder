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
    
    public delegate void PickupHandgunEventHandler();
    public static event PickupHandgunEventHandler HandgunPickedUp;
    
    void Start()
    {
        
    }

    protected virtual void OnHandgunPicked()
    {
        if (HandgunPickedUp != null)
        {
            HandgunPickedUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (CompareTag("Handgun"))
        {
            OnHandgunPicked();
        }
        else if (CompareTag("Machinegun"))
        {
            
        }
        
        
   
        Destroy(gameObject);
      

    }

}
