using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

enum FireState
{
    Ready, 
    Fireing, 
    Reloading
}

public class GunScript_ML : MonoBehaviour
{
    private FireState _fireState;

    [SerializeField] GameObject bullet;
    private GameObject ownedBullet;
    private Vector3 currentForward;
//    public WeaponEquip theWeapon;
    

    public void UnequipGun()
    {
        Destroy(gameObject);
    }
    
    public void FirePlayerGun(float amountDelay)
    {
        if (_fireState == FireState.Ready)
        {
            ownedBullet = Instantiate(bullet);
            ownedBullet.transform.position = transform.position;
            ownedBullet.GetComponent<BulletScript_ML>().fire = true;
            _fireState = FireState.Fireing;

            StartCoroutine(ShootDelay(amountDelay));
        }
    }
    

    public IEnumerator ShootDelay(float amountDelay)
    {
        yield return new WaitForSeconds(amountDelay);
        _fireState = FireState.Ready;
    }

}