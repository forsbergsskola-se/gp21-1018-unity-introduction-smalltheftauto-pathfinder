using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyScript_ML : MonoBehaviour
{
    [SerializeField] int _amountMoney;
    
    
    void Start()
    {
        // TODO: Try to avoid static events in the future. They have ruined two game projects that I've joined at a later point.
        // They made stuff like restarting the app a tremendous amount of work.
        PickupScript_ML.PickupPicked += MoneyGot;
        UIHealthbarScript_ML.OnPlayerDeath += PlayerDies;
        SaveSystem.OnGatherSaveData += SendSaveDataToSaveSystem;
        SaveSystem.OnSendSingleInt += ReceiveSaveData;
    }

    private void ReceiveSaveData(int amountMoney, DataType dataType)
    {
        if (dataType == DataType.Money)
        {
            _amountMoney = amountMoney;
            PrintMoney();
        }
    }
    
    private void SendSaveDataToSaveSystem()
    {
        SaveSystem.CurrentMoney = _amountMoney;
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
