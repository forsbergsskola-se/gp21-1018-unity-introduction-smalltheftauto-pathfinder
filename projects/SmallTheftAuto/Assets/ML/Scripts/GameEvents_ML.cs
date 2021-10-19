using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents_ML : MonoBehaviour
{

    public static GameEvents_ML Current;
    
    public delegate void Pickup();
    public static event Pickup APickup;


    public void OnPickup()
    {
        if (APickup != null)
        {
            APickup();
        }
    }
    
    
    private void Awake()
    {
        Current = this;
    }

    public event Action PickupHandgun;

    public void OnPickupHandgun()
    {
        if (PickupHandgun != null)
        {
            PickupHandgun();
        }
    }
}
