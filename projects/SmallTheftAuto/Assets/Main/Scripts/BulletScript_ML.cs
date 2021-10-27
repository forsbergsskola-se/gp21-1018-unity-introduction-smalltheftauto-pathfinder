using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript_ML : MonoBehaviour
{
    private bool fire = false;
    private Vector3 originalPos;
    private Vector3 originalForward;
 
    
    public delegate void FireGunEvent(WeaponEquip weaponEquip);
    public static event FireGunEvent FireGun;


    private void OnFireGun(WeaponEquip weaponEquip)
    {
        if (FireGun != null)
        {
            FireGun(weaponEquip);
        }
    }

    public void FireSetup(Transform originalTrans, Vector3 forward)
    {
        originalPos = originalTrans.position;
        originalForward = forward;
        transform.rotation = originalTrans.rotation;
        transform.Rotate(0,0,0);
     
        fire = true;
    }
    void Start()
    {
        OnFireGun(GunArmScript_ML._weaponEquip);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Handgun"))
        { 
            Destroy(gameObject);    
        }
    }
    
    void Update()
    {
        if (fire)
        {
            transform.position += originalForward * 8.0f * Time.deltaTime;
            
            if (Vector3.Distance(originalPos, transform.position) > 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
