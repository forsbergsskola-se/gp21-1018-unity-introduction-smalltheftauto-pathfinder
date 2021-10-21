using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmState
{
    Lowered,
    Raised
}

public enum WeaponEquip
{
    Fists,
    Handgun,
    Machinegun
}

public class GunArmScript_ML : MonoBehaviour
{
    private ArmState theArmState;

    private WeaponEquip _weaponEquip;
    
    private GameObject socket;

    private GameObject theGun;
 
    private bool gunEqupied = false;
    
    [SerializeField] private GameObject handgunEquip;
    [SerializeField] private GameObject machinegunEquip;
    private GameObject KeepGun;
    private GameObject machinegun;
   
    public delegate void SwicthedWeaponsEvent(AmmoType ammoType);

    public static event SwicthedWeaponsEvent SwitchedWeapons;
    
    public delegate void FireGunEvent(WeaponEquip weaponEquip);

    public static event FireGunEvent FireGun;


    public void OnFireGun(WeaponEquip weaponEquip)
    {
        if (FireGun != null)
        {
            FireGun(weaponEquip);
        }
    }
    
    public void OnSwitchedWeapon(AmmoType ammoType)
    {
        if (SwitchedWeapons != null)
        {
            SwitchedWeapons(ammoType);
        }
    }
    
    void Start()
    {
        PickupScript_ML.PickupPicked += PickedUpGun;
        
        socket = GameObject.FindWithTag("PlayerGunSocket");
    }

    public void PickedUpGun(PickupTypes pickupTypes)
    {
        gunEqupied = true;
    }
    

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if ( theArmState == ArmState.Lowered)
            {
                transform.Rotate(-90, 0, 0);
                theArmState = ArmState.Raised;
                StartCoroutine("Delay");
            }
        
            if (theArmState == ArmState.Raised && gunEqupied)
            {
                if (GetAmmoCount())
                {
                    GetComponentInChildren<GunScript_ML>().FirePlayerGun();
                    OnFireGun(_weaponEquip);
                }
            }
            
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            if (_weaponEquip != WeaponEquip.Fists)
            {
                _weaponEquip = WeaponEquip.Fists;
                UnequipWeapon();
                OnSwitchedWeapon(AmmoType.Fists);
            }
        }
        
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (PlayerInventory_ML.ownedGuns.Contains(OwnedGuns.Handgun))
            {
                if (_weaponEquip != WeaponEquip.Handgun)
                {
                    _weaponEquip = WeaponEquip.Handgun;
                    EquipHandgun();
                    OnSwitchedWeapon(AmmoType.Handgun);
                }
            }
        }
        
    }

    private bool GetAmmoCount()
    {
        bool outBool = false;

        switch (_weaponEquip)
        {
            case WeaponEquip.Handgun:

                outBool = PlayerInventory_ML.NumberHandgunBullets > 0;
                break;
            case WeaponEquip.Machinegun:
                outBool = PlayerInventory_ML.NumberMachinegunBullets > 0;
                break;
        }
      
        return outBool;
    }

    private void UnequipWeapon()
    {
        GetComponentInChildren<GunScript_ML>().UnequipGun();
    }
    
    private void EquipHandgun()
    {
       GameObject handgun = Instantiate(handgunEquip);
        handgun.transform.parent = socket.transform;
        handgun.transform.position = socket.transform.position;
        handgun.transform.rotation = socket.transform.rotation;
        handgun.transform.Rotate(0,-90,-90);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        transform.Rotate(90, 0, 0);
        theArmState = ArmState.Lowered;
    }
}
