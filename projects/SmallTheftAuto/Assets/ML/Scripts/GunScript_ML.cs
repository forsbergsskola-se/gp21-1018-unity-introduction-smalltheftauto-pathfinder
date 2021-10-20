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
    private GameObject barrel;
    [SerializeField] GameObject bullet;
    private GameObject ownedBullet;
    private Vector3 currentForward;
    
    public delegate void BulletFiredEventHandler(Vector3 forwardVector);

    public static event BulletFiredEventHandler BulletFired;


    public void OnBulletFired(Vector3 forwardVector)
    {
        if (BulletFired != null)
        {
            BulletFired(forwardVector);
        }
    }
    
    void Start()
    {
        barrel = GameObject.FindWithTag("GunExit");
        Movement_ML.PlayerGunFired += FirePlayerGun;
        
    }


    private void OnDestroy()
    {
        Movement_ML.PlayerGunFired -= FirePlayerGun;
        
    }

    public void FirePlayerGun(Vector3 forwardVector)
    {
        if (_fireState == FireState.Ready)
        {
            ownedBullet = Instantiate(bullet);
            ownedBullet.transform.position = barrel.transform.position;
            ownedBullet.GetComponent<BulletScript_ML>().forward = forwardVector;
            ownedBullet.GetComponent<BulletScript_ML>().fire = true;
            _fireState = FireState.Fireing;
            
            StartCoroutine(ShootDelay());
        }
    }
    

    public IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1);
        _fireState = FireState.Ready;
    }

}
