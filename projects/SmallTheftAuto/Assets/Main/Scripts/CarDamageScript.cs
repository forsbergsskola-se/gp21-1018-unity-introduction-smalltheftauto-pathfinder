using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public enum CarState
{
    Working,
    Broken
}

public class CarDamageScript : MonoBehaviour
{
    private float CarHealth = 100;
    private float currentSpeed = 0;
    private GameObject fireEmitter;
    private BoxCollider collider;
    private float damageModifier;
    private bool damageReady = true;
    private bool onFire = false;

    public delegate void ParticleEmittEvent(Vector3 positions,  bool isRandom);

    public static event ParticleEmittEvent OnParticleEmitt;
    
    public delegate void CarDestroyedEvent();
    public static event CarDestroyedEvent OnCarDestroyed;


    private void CarDestroyed()
    {
        if (OnCarDestroyed != null)
        {
            OnCarDestroyed();
        } 
    }
    
    private void ParticleEvent(Vector3 position, bool isRandom)
    {

        if (OnParticleEmitt != null)
        {
            OnParticleEmitt(position, isRandom);
        }
    }
    
    void Start()
    {
        fireEmitter = GameObject.FindWithTag("Fire");
        collider = GetComponent<BoxCollider>();
        Vehicle.OnCarSpeeding += CarIsSpeeding;
        //    fireEmitter.GetComponent<FireEmitter>().SetEmit(transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (damageReady)
        {
            CarHealth -= damageModifier;
            Debug.Log("Car health:" + CarHealth);
            damageReady = false;
            StartCoroutine(DelayMoreDamage());
        }
    }

    IEnumerator DelayMoreDamage()
    {
        yield return new WaitForSeconds(1);
        damageReady = true;
    }

    private void CarIsSpeeding(float speed)
    {
        damageModifier = speed * 5;

        if (damageModifier < 0)
        {
            damageModifier = Math.Abs(damageModifier);
        }
    }

    void Update()
    {
        if (CarHealth < 1)
        {
            if (!onFire)
            {
                float input = 100 * Time.deltaTime;
                int ins = (int) input;
                Vector3 randVector = new Vector3(Randomize._random.Next(-2, 2), 0, Randomize._random.Next(-1, 1));
                Vector3 pos = transform.position  + randVector;
                ParticleEvent(pos, true);
                StartCoroutine(DelayPutOutFire());
                onFire = true;
            }
        }

        if (CarHealth <= 0)
        {
            CarDestroyed();
        }
    }


    IEnumerator DelayPutOutFire()
    {
        yield return new WaitForSeconds(2);
        onFire = true;
    }
}
