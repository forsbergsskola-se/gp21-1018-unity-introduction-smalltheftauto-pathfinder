using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[System.Serializable]
public class SaveSystem : MonoBehaviour
{
    [SerializeField] int CurrentHeartHalves;
    [SerializeField] int CurrentMoney;
    //Last spawn point location
    [SerializeField] float x,y,z;
    
    string _path = "Assets/SaveFiles/SaveFile.txt";

    private void OnTriggerEnter(Collider other)
    {
        WriteToDoc();
    }

    private void WriteToDoc()
    {
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
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
