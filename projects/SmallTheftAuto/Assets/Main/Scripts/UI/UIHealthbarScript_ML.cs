using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public enum HeartState
{
    Full,
    Half,
    Empty
}

public class UIHealthbarScript_ML : MonoBehaviour
{

    Dictionary<int, HeartState> _health;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;
    private bool delayTrigger;

    public delegate void PlayerDeathEvent();
    public static event PlayerDeathEvent OnPlayerDeath;


    private void PlayerDeath()
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
        ResetHealth();
    }

    public bool CheckForDeath()
    {
        bool isDead = true;

        foreach (var el in _health)
        {
            if (el.Value != HeartState.Empty)
            {
                isDead = false;
            }
        }

        return isDead;
    }
    
    void Start()
    {
        PickupScript_ML.PickupPicked += HealPlayer;
        PainVolumeScript_ML.PainEvent += DecrementHeart;
        SaveSystem.OnGatherData += SendDataToSaveSystem;
        FillHearts();
    }

    private void SendDataToSaveSystem()
    {
        SaveSystem.CurrentHeartHalves = CountHearts();
    }

    private int CountHearts()
    {
        int heartCount = 0;
        
        foreach (var el in _health)
        {
            if (el.Value == HeartState.Full)
            {
                heartCount += 2;
            }
            else if (el.Value == HeartState.Half)
            {
                heartCount++;
            }
        }

        return heartCount;
    }
    
    private void HealPlayer(PickupTypes pickupType)
    {
        if (pickupType == PickupTypes.Health)
        {
            for (int i = 0; i < 10; i++)
            {
                IncrementHeart();
            }
        }
    }
    
    
    
    private void ChangeHeart(int heartIndex, HeartState heartType)
    {
        Image imageComponent = GetComponentsInChildren<Image>()[heartIndex];

        switch (heartType)
        {
            case HeartState.Full:
                imageComponent.sprite = heartFull;
                break;
            case HeartState.Half:
                imageComponent.sprite = heartHalf;
                break;
            case HeartState.Empty:
                imageComponent.sprite = heartEmpty;
                break;
        }
    }

    private void IncrementHeart()
    {
        
        for (int i = 0; i < 10; i++)
        {
            if (_health[i] != HeartState.Full)
            {
                if (_health[i] == HeartState.Half)
                {
                    _health[i] = HeartState.Full;
                }
                else
                {
                    _health[i] = HeartState.Half;
                }
                break;
            }
            
            foreach (var el in _health)
            {
                ChangeHeart(el.Key, el.Value);
            }
        }
    }
    
    private void DecrementHeart()
    {
        for (int i = 9; i >= 0; i--)
        {
            if (_health[i] != HeartState.Empty)
            {
                if (_health[i] == HeartState.Full)
                {
                    _health[i] = HeartState.Half;
                }
                else
                {
                    _health[i] = HeartState.Empty;
                }
                break;
            }
        }
        
        foreach (var el in _health)
        {
            ChangeHeart(el.Key, el.Value);
        }

        if (CheckForDeath())
        {
            PlayerDeath();
        }
    }
    
    private void ChangeHeartLevel(int heartHalves, bool increaseOrDecrease)
    {
        
        for (int i = 0; i < heartHalves; i++)
        {
            if(increaseOrDecrease)
            {
               IncrementHeart();
            }
            else
            {
                DecrementHeart();     
            }
        }
        
        foreach (var el in _health)
        {
            ChangeHeart(el.Key, el.Value);
        }
    }

    private void ResetHealth()
    {
        _health.Clear();
        _health = new Dictionary<int, HeartState>(10);
        for (int i = 0; i < 10; i++)
        {
            _health.Add(i, HeartState.Full);
        }
        
        foreach (var el in _health)
        {
            ChangeHeart(el.Key, el.Value);
        }
    }
    
    private void FillHearts()
    {
        _health = new Dictionary<int, HeartState>(10);

        for (int i = 0; i < 10; i++)
        {
            _health.Add(i, HeartState.Full);
        }
    }

    private IEnumerator DelayHeal(float delayAmount)
    {
        yield return new WaitForSeconds(delayAmount);
        delayTrigger = true;
    }
}
