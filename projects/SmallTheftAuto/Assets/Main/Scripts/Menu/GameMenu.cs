using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Button startButton, loadButton;
    public int CurrentHeartHalves;
    public int CurrentMoney;
    //Last spawn point location
    public float x,y,z;
    
    string _path = "Assets/SaveFiles/SaveFile.txt";

    void Start()
    {
        startButton.onClick.AddListener(LoadLevel);
    }

    public void LoadGame()
    {
        ReadFromFile();
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    private void ReadFromFile()
    {
        StreamReader reader = new StreamReader(_path);
        CurrentHeartHalves = Convert.ToInt32(reader.ReadLine());
        CurrentMoney = Convert.ToInt32(reader.ReadLine());
        x = Convert.ToSingle(reader.ReadLine());
        y = Convert.ToSingle(reader.ReadLine());
        z = Convert.ToSingle(reader.ReadLine());
        
        reader.Close();

    }
}
