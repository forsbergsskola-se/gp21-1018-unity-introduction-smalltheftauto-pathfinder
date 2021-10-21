using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum AmmoType
{
    Handgun,
    Machinegun,
    Fists
}

public enum SelectedGun
{
    Handgun,
    Machinegun
}

public enum OwnedGuns
{
    Handgun,
    Machinegun
}

public class PlayerInventory_ML : MonoBehaviour
{
    public static List<OwnedGuns> ownedGuns { get; private set; }
    private AmmoType ammoType;
    public static int NumberHandgunBullets { get; private set; }
    private static int MaxNumberHandgunBullets = 300;
    private static int MaxNumberMachineginBullets = 200;
    public static int NumberMachinegunBullets  { get; private set; }
    
    private Text AmmoCounter;
    private Text HealthCounter;
    
    
    
    
    private void GunPickedUp(PickupTypes pickupTypes)
    {

        if (pickupTypes == PickupTypes.Handgun)
        {
            NumberHandgunBullets += 10;

            if (NumberHandgunBullets > MaxNumberHandgunBullets)
            {
                NumberHandgunBullets = MaxNumberHandgunBullets;
            }

            if (!ownedGuns.Contains(OwnedGuns.Handgun))
            {
                ownedGuns.Add(OwnedGuns.Handgun);
            }
        }
        
        else if (pickupTypes == PickupTypes.Machinegun)
        {
            NumberMachinegunBullets += 20;

            if (NumberMachinegunBullets > MaxNumberMachineginBullets)
            {
                NumberMachinegunBullets = MaxNumberMachineginBullets;
            }

            if (!ownedGuns.Contains(OwnedGuns.Machinegun))
            {
                ownedGuns.Add(OwnedGuns.Machinegun);
            }
        }
        
    }

    private void DecrementAmmo(WeaponEquip theAmmoType)
    {
        switch (theAmmoType)
        {
            case WeaponEquip.Handgun:
                if (NumberHandgunBullets > 0)
                {
                    NumberHandgunBullets--;
                }
                break;
            case WeaponEquip.Machinegun:
                if (NumberMachinegunBullets > 0)
                {
                    NumberMachinegunBullets--;
                }
                break;
        }
        PrintAmmoCounter(AmmoType.Handgun);
    }
    
    void Start()
    {
        GunArmScript_ML.FireGun += DecrementAmmo;
        GunArmScript_ML.SwitchedWeapons += PrintAmmoCounter;
        PickupScript_ML.PickupPicked += GunPickedUp;
        HealthCounter = GameObject.Find("Health").GetComponent<Text>();
        AmmoCounter = GameObject.FindWithTag("AmmoCounter").GetComponent<Text>();
      
        AmmoCounter.fontSize = 23;
        
        ownedGuns = new List<OwnedGuns>(2);
    }


    public void PrintAmmoCounter(AmmoType selectedGun)
    {
        
        if (selectedGun == AmmoType.Handgun && ownedGuns.Contains(OwnedGuns.Handgun))
        {
            AmmoCounter.text = Convert.ToString(NumberHandgunBullets) 
                               + " / " + Convert.ToString(MaxNumberHandgunBullets);
        }
        
        else if (selectedGun == AmmoType.Machinegun&& ownedGuns.Contains(OwnedGuns.Machinegun))
        {
            AmmoCounter.text = Convert.ToString(NumberMachinegunBullets)
                               + " / " + Convert.ToString(MaxNumberMachineginBullets);
        }
        else if (selectedGun == AmmoType.Fists)
        {
            AmmoCounter.text = " ";
        }
        
    }
    
}
