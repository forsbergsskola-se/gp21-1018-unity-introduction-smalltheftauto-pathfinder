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

public class UI_Display_ML : MonoBehaviour
{
    private AmmoType ammoType;
    private int NumberHandgunBullets = 0;
    private int MaxNumberHandgunBullets = 300;
    private int MaxNumberMachineginBullets = 200;
    private int NumberMachinegunBullets = 0;
    
    private Canvas canvas;
    private Text AmmoCounter;
    
    private void GunPickedUp(string gunType)
    {

        if (gunType == "Handgun")
        {
            NumberHandgunBullets += 10;
        }

        if (ammoType == AmmoType.Handgun)
        {
            PrintAmmoCounter();
        }
    }
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
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
