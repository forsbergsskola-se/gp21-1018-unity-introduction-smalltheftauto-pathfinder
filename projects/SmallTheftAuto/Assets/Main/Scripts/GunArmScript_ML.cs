using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmState
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

public enum AmmoCheck
{
    Reload,
    Shoot
}

public class GunArmScript_ML : MonoBehaviour
{
    private ArmState theArmState;
    private AmmoCheck _ammoCheck;

    private static List<WeaponEquip> ownedWeapons;

    private UIWeaponScript_ML weaponScript;

    public static WeaponEquip _weaponEquip { get; private set; }
    
    private GameObject socket;

    [SerializeField] private GameObject handgunEquip;
    [SerializeField] private GameObject machinegunEquip;
    private GameObject currentGun;

    public delegate void SwicthedWeaponsEvent(WeaponEquip selectedWeapon);
    public static event SwicthedWeaponsEvent SwitchedWeapons;


    public delegate void ReloadWeaponEvent(WeaponEquip weaponEquip);

    public static event ReloadWeaponEvent OnReloadWeapon;
    
    
    
    private void OnSwitchedWeapon(WeaponEquip selectedWeapon)
    {
        if (SwitchedWeapons != null)
        {
            SwitchedWeapons(selectedWeapon);
        }
    }
    
    void Start()
    {
        socket = GameObject.FindWithTag("PlayerGunSocket");
        weaponScript = GameObject.FindWithTag("AmmoCounter").GetComponent<UIWeaponScript_ML>();
    }


    private void FireWeapon()
    {
        Vector3 forward = GetComponentInParent<PlayerMovement_ML>().GetComponent<Transform>().forward;   
        if ( _weaponEquip == WeaponEquip.Handgun)
        {
            if (DoesHaveAmmoInClip())
            {
                GetComponentInChildren<GunScript_ML>().FireGun(0.7f, forward);
            }
        }
        
        else if  (_weaponEquip == WeaponEquip.Machinegun)
        {
            if (DoesHaveAmmoInClip())
            {
                GetComponentInChildren<GunScript_ML>().FireGun(0.2f, forward);
            }
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PrepArmState();
        
            FireWeapon();
        }
       
        else if (Input.GetButton("Fire1"))
        {
            PrepArmState();
            
           FireWeapon();
        }
        
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if(DoesHaveAmmoToReload())
            {
                ReloadGun(_weaponEquip);
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnequipWeapon();
            if (_weaponEquip != WeaponEquip.Fists)
            {
                _weaponEquip = WeaponEquip.Fists;
                OnSwitchedWeapon(_weaponEquip);
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponScript.ownedGuns.Contains(WeaponEquip.Handgun))
            {
                UnequipWeapon();
                if (_weaponEquip != WeaponEquip.Handgun)
                {
                    _weaponEquip = WeaponEquip.Handgun;
                    EquipWeapon();
                    OnSwitchedWeapon(_weaponEquip);
                }
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weaponScript.ownedGuns.Contains(WeaponEquip.Machinegun))
            {
                UnequipWeapon();
                if (_weaponEquip != WeaponEquip.Machinegun)
                {
                    _weaponEquip = WeaponEquip.Machinegun;
                    EquipWeapon();
                    OnSwitchedWeapon(_weaponEquip);
                }
            }
        }
        
    }

   
    
    IEnumerator MachineGunBulletSpacing()
    {
        yield return new WaitForSeconds(0.1f);
    }
    
    
    private void PrepArmState()
    {
        if ( theArmState == ArmState.Lowered)
        {
            transform.Rotate(-90, 0, 0);
            theArmState = ArmState.Raised;
            StartCoroutine("DelayLower");
        }
    }

    private void ReloadGun(WeaponEquip weaponEquip)
    {
        if (OnReloadWeapon != null)
        {
            OnReloadWeapon(weaponEquip);
        }
    }

    private bool DoesHaveAmmoInClip()
    {
        bool outBool = false;
        
        switch (_weaponEquip)
        {
            case WeaponEquip.Handgun:
                
                outBool = weaponScript.CurrentHandgunClip > 0;
                break;
           
            case WeaponEquip.Machinegun:
                outBool = weaponScript.CurrentMachineGunClip > 0;
                break;
        }
      
        return outBool;
    }
    
    private bool DoesHaveAmmoToReload()
    {
        bool outBool = false;
        
        switch (_weaponEquip)
        {
            case WeaponEquip.Handgun:
                
                outBool = weaponScript.NumberHandgunBullets > 0;
                break;
           
            case WeaponEquip.Machinegun:
                outBool = weaponScript.NumberMachinegunBullets > 0;
                break;
        }
      
        return outBool;
    }

    private void UnequipWeapon()
    {
        if(_weaponEquip != WeaponEquip.Fists)
            GetComponentInChildren<ObjectDestructor>().DestroyObject();
    }
    
    private void EquipWeapon()
    {
        switch (_weaponEquip)
        {
            case WeaponEquip.Handgun:
                currentGun = Instantiate(handgunEquip);
                break;
            case WeaponEquip.Machinegun:
                currentGun = Instantiate(machinegunEquip);
                break;
        }

        currentGun.transform.parent = socket.transform;
        currentGun.transform.position = socket.transform.position;
        currentGun.transform.rotation = socket.transform.rotation;
        currentGun.transform.Rotate(0,-90,-90);
    }

    private IEnumerator DelayLower()
    {
        yield return new WaitForSeconds(3);
        transform.Rotate(90, 0, 0);
        theArmState = ArmState.Lowered;
    }
}
