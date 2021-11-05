using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PainVolumeScript_ML : MonoBehaviour
{
    public delegate void PainVolumeEvent(float damagAmount);
    public static event PainVolumeEvent PainEvent;
    private bool painReady = true;

    private void OnPlayerPainEvent(float damageAmount)
    {
        if (PainEvent != null)
        {
            PainEvent(damageAmount);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("ThePlayer"))
        {
            if (painReady)
            {
                OnPlayerPainEvent(1);
                painReady = false;
                StartCoroutine(DelayPain());
            }
        }
    }

    private  IEnumerator DelayPain()
    {
        yield return new WaitForSeconds(0.1f);
        painReady = true;
    }
 
}
