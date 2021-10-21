using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmState
{
    Lowered,
    Raised
}

enum WeaponEquip
{
    Fists,
    Handgun,
    Machinegun
}

public class GunArmScript_ML : MonoBehaviour
{
    private ArmState theArmState;

    private WeaponEquip _weaponEquip;
    
    private GameObject socket;

    private GameObject theGun;
 
    private bool gunEqupied = false;
    
    [SerializeField] private GameObject handgunEquip;
    [SerializeField] private Component gun;
    private GameObject handgun;
   
    void Start()
    {
        PickupScript_ML.PickupPicked += PickedUpGun;
        
        socket = GameObject.FindWithTag("PlayerGunSocket");
    }

    public void PickedUpGun(string gunType)
    {
        gunEqupied = true;
    }
    

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if ( theArmState == ArmState.Lowered)
            {
                transform.Rotate(-90, 0, 0);
                theArmState = ArmState.Raised;
                StartCoroutine("Delay");
            }
        
            if (theArmState == ArmState.Raised && gunEqupied)
            {
                GetComponentInChildren<GunScript_ML>().FirePlayerGun();
            }
            
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (PlayerInventory_ML.ownedGuns.Contains(OwnedGuns.Handgun))
            {
                if (_weaponEquip != WeaponEquip.Handgun)
                {
                    _weaponEquip = WeaponEquip.Handgun;
                    EquipHandgun();
                }
            }
        }
        
    }

    public void EquipHandgun()
    {
        handgun = Instantiate(handgunEquip);
        handgun.transform.parent = socket.transform;
        handgun.transform.position = socket.transform.position;
        handgun.transform.rotation = socket.transform.rotation;
        handgun.transform.Rotate(0,-90,-90);
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        transform.Rotate(90, 0, 0);
        theArmState = ArmState.Lowered;
    }
}
