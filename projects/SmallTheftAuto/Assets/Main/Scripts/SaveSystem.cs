using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class SaveSystem : MonoBehaviour
{
    public static int CurrentHeartHalves;
    public static int CurrentMoney;
    //Last spawn point location
    public static float x,y,z;
    
    string _path = "Assets/SaveFiles/SaveFile.txt";

    public delegate void GatherDataEvent();
    public static event GatherDataEvent OnGatherSaveData;
    
    public delegate void SendIntEvent(int data, DataType dataType);
    public static event SendIntEvent OnSendSingleInt;
    
    public delegate void SendVectorEvent(Vector3 data);
    public static event SendVectorEvent OnSendVector;
    
    
    private void OnTriggerEnter(Collider other)
    {
        GatherSaveData();
        WriteToDoc();
    }

    private void GatherSaveData()
    {
        if (OnGatherSaveData != null)
        {
            OnGatherSaveData();
        }

    }
    
    private void SetDefaultSpawnPoint()
    {
        x = -47;
        y = 0;
        z = -6.7f;
    }
    
    public void SendSaveData()
    {
        ReadFromFile();
        
        if (OnSendSingleInt != null)
        {
            OnSendSingleInt(CurrentMoney, DataType.Money);
            OnSendSingleInt(CurrentHeartHalves, DataType.Health);
        }

        if (x == 0 && y == 0 && z == 0)
        {
            Debug.Log("Default spawn point set");
            SetDefaultSpawnPoint();
        }

        StartCoroutine(WaitForSpawner());
    }
        
    private  IEnumerator WaitForSpawner()
    {
        yield return new WaitUntil(() => PlayerSpawnerScript_ML.SpawnerReady);
        if (OnSendVector != null)
        {
            OnSendVector(new Vector3(x, y, z));
        }
        else
        {
            Debug.Log("Nobody listens to Send vector");
        }
    }
    
    private void LoadGame()
    {
        SendSaveData();
    }

   // [MenuItem("Tools/Write file")]
    private void WriteToDoc()
    {
        
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
        
        StreamWriter writer = new StreamWriter(_path, true);
        writer.WriteLine(CurrentHeartHalves);
        writer.WriteLine(CurrentMoney);
        writer.WriteLine(x);
        writer.WriteLine(y);
        writer.WriteLine(z);
        
        writer.Close();
        
        AssetDatabase.ImportAsset(_path); 
    //    TextAsset asset = Resources.Load("test");
    }
    
    
    //[MenuItem("Tools/Read file")]
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
    
    void Start()
    {
        if (File.Exists(_path) && PlayerPrefs.GetString("GameType") == "Load")
        {
            LoadGame();
        }
        
    }

}
