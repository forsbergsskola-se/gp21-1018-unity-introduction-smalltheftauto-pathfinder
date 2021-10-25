using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyScript_ML : MonoBehaviour
{
    private int _amountMoney;
    
    
    void Start()
    {
        PickupScript_ML.PickupPicked += MoneyGot;
        UIHealthbarScript_ML.OnPlayerDeath += PlayerDies;
    }

    private void PlayerDies()
    {
        _amountMoney /= 2;
        PrintMoney();
    }

    private void PrintMoney()
    {
        GetComponentInChildren<Text>().text = Convert.ToString(_amountMoney);
    }
    
    private void MoneyGot(PickupTypes pickupType)
    {
        if (pickupType == PickupTypes.Money)
        {
            _amountMoney += 500;
            PrintMoney();
        }
    }

}
