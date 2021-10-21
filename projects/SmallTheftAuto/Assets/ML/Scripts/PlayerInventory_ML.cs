using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum AmmoType
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
    
    private void GunPickedUp(string gunType)
    {

        if (gunType == "Handgun")
        {
            NumberHandgunBullets += 10;

            if (!ownedGuns.Contains(OwnedGuns.Handgun))
            {
                ownedGuns.Add(OwnedGuns.Handgun);
            }
        }

        if (ammoType == AmmoType.Handgun)
        {
            PrintAmmoCounter();
        }
    }
    
    void Start()
    {
        ownedGuns = new List<OwnedGuns>(2);
        AmmoCounter = GetComponent<Text>();
        PickupScript_ML.PickupPicked += GunPickedUp;
        
        AmmoCounter.fontSize = 50;
    }


    public void PrintAmmoCounter()
    {
        if (ammoType == AmmoType.Handgun)
        {
            AmmoCounter.text = Convert.ToString(NumberHandgunBullets)
                               + " / " + Convert.ToString(MaxNumberHandgunBullets);
        }
        
        else if (ammoType == AmmoType.Machinegun)
        {
            AmmoCounter.text = Convert.ToString(NumberMachinegunBullets)
                               + " / " + Convert.ToString(MaxNumberMachineginBullets);
        }
    }
    
}
