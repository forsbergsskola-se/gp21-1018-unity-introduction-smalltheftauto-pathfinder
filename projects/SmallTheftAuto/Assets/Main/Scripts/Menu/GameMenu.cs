using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DataType
{
    Health,
    Money,
    Location
}

public class GameMenu : MonoBehaviour
{
    private DataType _dataType;
    public Button startButton, loadButton;
    public int CurrentHeartHalves;
    public int CurrentMoney;
    //Last spawn point location
    public float x,y,z;

    string _path = "Assets/SaveFiles/SaveFile.txt";
    

    void Start()
    {
        startButton.onClick.AddListener(NewGame);
        loadButton.onClick.AddListener(LoadGame);
    }

    private void SetDefaultSpawnPoint()
    {
        x = -47;
        y = 0;
        z = -6.7f;
    }

    public void NewGame()
    {
        PlayerPrefs.SetString("GameType", "New");
        
        LoadLevel();
    }
    
    public void LoadGame()
    {
        if (File.Exists(_path))
        {
            PlayerPrefs.SetString("GameType", "Load");
            ReadFromFile();

            LoadLevel();
            
        }
        else
        {
            GameObject.Find("PlayerMessage").
            GetComponentInChildren<Text>().text = "Save File Not Found";
            StartCoroutine(DelayRemoveMessage());
        }
    }

    private IEnumerator DelayRemoveMessage()
    {
        yield return new WaitForSeconds(3);
        
        GameObject.Find("PlayerMessage").
            GetComponentInChildren<Text>().text = "";
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
