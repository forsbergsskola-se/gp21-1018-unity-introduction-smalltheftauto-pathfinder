using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDamageScript : MonoBehaviour
{
    private float CarHealth = 100;
    private float currentSpeed = 0;
    private GameObject fireEmitter;
    private BoxCollider collider;
    private float damageModifier;
    
    void Start()
    {
        fireEmitter = GameObject.FindWithTag("Fire");
        collider = GetComponent<BoxCollider>();
        Vehicle.OnCarSpeeding += CarIsSpeeding;
        //    fireEmitter.GetComponent<FireEmitter>().SetEmit(transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        CarHealth -= damageModifier;
        Debug.Log("Car health:" + CarHealth);
    }

    private void CarIsSpeeding(float speed)
    {
        damageModifier = speed * 5;
    }

    void Update()
    {
        if (CarHealth < 90)
        {
            fireEmitter.GetComponent<FireEmitter>().SetEmit(transform.position);
        }
    }
}
