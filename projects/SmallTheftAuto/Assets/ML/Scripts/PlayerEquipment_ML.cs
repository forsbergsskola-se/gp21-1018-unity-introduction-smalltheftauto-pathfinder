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
        
        PickupScript_ML.PickupPicked += FirstPickup;
    }



    void FirstPickup(string type)
    {

        handgun = Instantiate(handgunEquip);
        handgun.transform.parent = socket.transform;
        handgun.transform.position = socket.transform.position;
        handgun.transform.rotation = socket.transform.rotation;
        handgun.transform.Rotate(0,-90,-90);

        //    socket.AddComponent(handgunPickup.GetType());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
