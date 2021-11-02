using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorldSpaceCanvas : MonoBehaviour
{
    private Text playerMessage;
    private Canvas theCanvas;
    void Start()
    {
        theCanvas = GetComponent<Canvas>();
        playerMessage = GetComponentInChildren<Text>();
        playerMessage.text = "Reload";
        
        
    }

    private void LateUpdate()
    {
        theCanvas.transform.position = GameObject.FindWithTag("ThePlayer").transform.position;
        theCanvas.transform.rotation = GameObject.FindWithTag("ThePlayer").transform.rotation;
    }
}
