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

    private void OnPainEvent()
    {
        if (PainEvent != null)
        {
            PainEvent();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (painReady)
        {
            OnPainEvent();
            painReady = false;
            StartCoroutine(DelayPain());
        }
    }

    private  IEnumerator DelayPain()
    {
        yield return new WaitForSeconds(1.0f);
        painReady = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
