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

public class HealthbarScript_ML : MonoBehaviour
{

    Dictionary<int, HeartState> _health;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;

    private Image imageComponent;
    // Start is called before the first frame update
    void Start()
    {
        PainVolumeScript_ML.PainEvent += DecrementHeart;
        FillHearts();
    }

    private void ChangeHeart(int heartIndex, HeartState heartType)
    {
        imageComponent = GetComponentsInChildren<Image>()[heartIndex];

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
    
    private void FillHearts()
    {
        _health = new Dictionary<int, HeartState>(10);

        for (int i = 0; i < 10; i++)
        {
            _health.Add(i, HeartState.Full);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
