using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIWeaponScript_ML : MonoBehaviour
{
    public static List<WeaponEquip> ownedGuns { get; private set; }
    public static int NumberHandgunBullets { get; private set; }
    private static int MaxNumberHandgunBullets = 200;
    private static int MaxNumberMachineginBullets = 300;
    public static int NumberMachinegunBullets  { get; private set; }
    private WeaponEquip currentWeapon;
    
    private Text AmmoCounter;

    [SerializeField] private Sprite HandgunSprite;
    [SerializeField] private Sprite MachinegunSprite;
    [SerializeField] private Sprite FistSprite;
    
    
    
    
    private void GunPickedUp(PickupTypes pickupTypes)
    {

        if (pickupTypes == PickupTypes.Handgun)
        {
            NumberHandgunBullets += 10;

            if (NumberHandgunBullets > MaxNumberHandgunBullets)
            {
                NumberHandgunBullets = MaxNumberHandgunBullets;
            }

            if (!ownedGuns.Contains(WeaponEquip.Handgun))
            {
                ownedGuns.Add(WeaponEquip.Handgun);
            }

            if (currentWeapon == WeaponEquip.Handgun)
            {
                UpdateAmmoCounter(WeaponEquip.Handgun);
            }
        }
        
        else if (pickupTypes == PickupTypes.Machinegun)
        {
            NumberMachinegunBullets += 20;

            if (NumberMachinegunBullets > MaxNumberMachineginBullets)
            {
                NumberMachinegunBullets = MaxNumberMachineginBullets;
            }

            if (!ownedGuns.Contains(WeaponEquip.Machinegun))
            {
                ownedGuns.Add(WeaponEquip.Machinegun);
            }
            
            if (currentWeapon == WeaponEquip.Machinegun)
            {
                UpdateAmmoCounter(WeaponEquip.Machinegun);
            }
        }
        
    }

    private void DecrementAmmo(WeaponEquip weaponType)
    {
        switch (weaponType)
        {
            case WeaponEquip.Handgun:
                if(NumberHandgunBullets > 0 )
                    NumberHandgunBullets--;
                break;
            
            case WeaponEquip.Machinegun:
                if(NumberMachinegunBullets > 0) 
                    NumberMachinegunBullets--;
                break;
        }
        UpdateAmmoCounter(weaponType);
    }
    
    void Start()
    {
        ownedGuns = new List<WeaponEquip>(3);
        ownedGuns.Add(WeaponEquip.Fists);
        BulletScript_ML.FireGun += DecrementAmmo;
        GunArmScript_ML.SwitchedWeapons += SwitchWeapons;
        PickupScript_ML.PickupPicked += GunPickedUp;
        AmmoCounter = GetComponentInChildren<Text>();

        AmmoCounter.fontSize = 23;
    }

    private void SwitchWeapons(WeaponEquip weaponEquip)
    {
        currentWeapon = weaponEquip;
        
        if (ownedGuns.Contains(weaponEquip))
        {
            ChangeSprite(weaponEquip);
            UpdateAmmoCounter(weaponEquip);
        }
    }
    
    private void ChangeSprite(WeaponEquip weaponEquip)
    {
        Image imageComponent = GetComponentInChildren<Image>();
        
        switch (weaponEquip)
        {
            case WeaponEquip.Fists:
                imageComponent.sprite = FistSprite;
                break;
            
            case WeaponEquip.Handgun:
                imageComponent.sprite = HandgunSprite;
                break;
            
            case WeaponEquip.Machinegun:
                imageComponent.sprite = MachinegunSprite;
                break;
        }
    }

    private void UpdateAmmoCounter(WeaponEquip selectedGun)
    {
        
        if (selectedGun == WeaponEquip.Handgun)
        {
            AmmoCounter.text = Convert.ToString(NumberHandgunBullets) 
                               + " / " + Convert.ToString(MaxNumberHandgunBullets);
        }
        
        else if (selectedGun == WeaponEquip.Machinegun)
        {
            AmmoCounter.text = Convert.ToString(NumberMachinegunBullets)
                               + " / " + Convert.ToString(MaxNumberMachineginBullets);
        }
        else if (selectedGun == WeaponEquip.Fists)
        {
            AmmoCounter.text = " ";
        }
        
    }
    
}
