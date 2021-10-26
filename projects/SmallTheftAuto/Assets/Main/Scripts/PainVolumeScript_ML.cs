using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PainVolumeScript_ML : MonoBehaviour
{
    public delegate void PainVolumeEvent();
    public static event PainVolumeEvent PainEvent;
    private bool painReady = true;

    private void OnPlayerPainEvent()
    {
        if (PainEvent != null)
        {
            PainEvent();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("ThePlayer"))
        {
            if (painReady)
            {
                OnPlayerPainEvent();
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
