using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript_ML : MonoBehaviour
{
    public bool fire = false;
    private Vector3 originalPos;
    private Transform originalTransform;
    private Vector3 originalForward;
    private BoxCollider collider;

    
    
    public delegate void FireGunEvent(WeaponEquip weaponEquip);
    public static event FireGunEvent FireGun;


    private void OnFireGun(WeaponEquip weaponEquip)
    {
        if (FireGun != null)
        {
            FireGun(weaponEquip);
        }
    }

    
    void Start()
    {
        originalPos = transform.position;
        originalForward = Movement_ML.PlayerTransform.forward ;
        
        transform.rotation = Movement_ML.PlayerTransform.rotation;
        transform.Rotate(90,0,0);
        collider = GetComponent<BoxCollider>();
        collider.enabled = true;
        
        OnFireGun(GunArmScript_ML._weaponEquip);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
    
    void Update()
    {
        if (fire)
        {
            transform.position += originalForward * 3.0f * Time.deltaTime;

            if (Vector3.Distance(originalPos, transform.position) > 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
