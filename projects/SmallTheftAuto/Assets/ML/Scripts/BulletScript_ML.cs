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
        originalTransform = Movement_ML.PlayerTransform;
        
        transform.rotation = originalTransform.rotation;
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
            transform.position += originalTransform.forward * 3.0f * Time.deltaTime;

            if (Vector3.Distance(originalPos, transform.position) > 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
