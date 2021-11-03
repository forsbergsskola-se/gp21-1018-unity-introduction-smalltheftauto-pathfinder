using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIWeaponScript_ML : MonoBehaviour
{
    public List<WeaponEquip> ownedGuns;
    public  int NumberHandgunBullets { get; private set; }
    public  int NumberMachinegunBullets  { get; private set; }
    
    private static int MaxNumberHandgunBullets = 200;
    private static int MaxNumberMachineginBullets = 300;

    public int CurrentHandgunClip{ get; private set; }
    public int CurrentMachineGunClip{ get; private set; }

    private int HandgunClip = 15;
    private int MachineGunClip = 25;

    private WeaponEquip currentWeapon;
    
    private Text AmmoCounter;

    [SerializeField] private Sprite HandgunSprite;
    [SerializeField] private Sprite MachinegunSprite;
    [SerializeField] private Sprite FistSprite;


    
    
    private void GunPickedUp(PickupTypes pickupTypes)
    {

        if (pickupTypes == PickupTypes.Handgun)
        {
            NumberHandgunBullets += 15;

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
            NumberMachinegunBullets += 30;

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
                if(CurrentHandgunClip > 0 )
                    CurrentHandgunClip--;
                break;
            
            case WeaponEquip.Machinegun:
                if(CurrentMachineGunClip > 0) 
                    CurrentMachineGunClip--;
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
        GunArmScript_ML.OnReloadWeapon += ReloadGun;
        PickupScript_ML.PickupPicked += GunPickedUp;
        AmmoCounter = GetComponentInChildren<Text>();

        AmmoCounter.fontSize = 23;
    }

    private void ReloadGun(WeaponEquip weaponEquip)
    {
        switch (weaponEquip)
        {
            case WeaponEquip.Handgun:
                if (NumberHandgunBullets - HandgunClip < 0)
                {
                    CurrentHandgunClip += NumberHandgunBullets;
                    NumberHandgunBullets -= NumberHandgunBullets;
                }
                else if (NumberHandgunBullets - HandgunClip >= 0)
                {
                    CurrentHandgunClip += HandgunClip;
                    NumberHandgunBullets -= HandgunClip;
                }
                break;
            
            case WeaponEquip.Machinegun:
                if (NumberMachinegunBullets - MachineGunClip < 0)
                {
                    CurrentMachineGunClip += NumberMachinegunBullets;
                    NumberMachinegunBullets -= NumberMachinegunBullets;
                }
                else if (NumberMachinegunBullets - MachineGunClip >= 0)
                {
                    CurrentMachineGunClip += MachineGunClip;
                    NumberMachinegunBullets -= MachineGunClip;
                }
                break;
        }
        UpdateAmmoCounter(weaponEquip);
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
            if (CurrentHandgunClip == 0)
            {
                AmmoCounter.text = "Reload" + " / " +
                                   Convert.ToString(NumberHandgunBullets);
            }
            else
            {
                AmmoCounter.text = Convert.ToString(CurrentHandgunClip) 
                                   + " / " + Convert.ToString(NumberHandgunBullets);
            }
        }
        
        else if (selectedGun == WeaponEquip.Machinegun)
        {
            if (CurrentMachineGunClip == 0)
            {
                AmmoCounter.text = "Reload" + " / " +
                                   Convert.ToString(NumberMachinegunBullets);
            }
            else
            {
                AmmoCounter.text = Convert.ToString(CurrentMachineGunClip) 
                                   + " / " + Convert.ToString(NumberMachinegunBullets);
            }
        }
        else if (selectedGun == WeaponEquip.Fists)
        {
            AmmoCounter.text = " ";
        }
        
    }
    
}
