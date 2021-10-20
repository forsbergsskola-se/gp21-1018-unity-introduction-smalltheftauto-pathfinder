using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEquipment_ML : MonoBehaviour
{
    private GameObject socket;

    [SerializeField] private GameObject handgunEquip;
    [SerializeField] private Component gun;
    private GameObject handgun;
    
    void Start()
    {
        socket = GameObject.FindWithTag("PlayerGunSocket");
        
    }



    void FirstPickup(string type)
    {
        

        //    socket.AddComponent(handgunPickup.GetType());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
