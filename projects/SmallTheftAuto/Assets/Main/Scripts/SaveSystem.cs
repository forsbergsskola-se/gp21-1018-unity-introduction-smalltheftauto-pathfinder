using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[System.Serializable]
public class SaveSystem : MonoBehaviour
{
    public static int CurrentHeartHalves;
    public static int CurrentMoney;
    //Last spawn point location
    public static float x,y,z;
    
    string _path = "Assets/SaveFiles/SaveFile.txt";

    public delegate void GatherDataEvent();
    public static event GatherDataEvent OnGatherData;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        GatherSaveData();
        WriteToDoc();
    }

    private void GatherSaveData()
    {
        if (OnGatherData != null)
        {
            OnGatherData();
        }

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
        
    }

}
