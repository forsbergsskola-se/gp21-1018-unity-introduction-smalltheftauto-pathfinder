using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGunArm : MonoBehaviour
{
    public ArmState armState;

    private int ShotsFired;
    private FireState _fireState;
    private GameObject currentGun;
    
    [SerializeField] private GameObject handgunEquip;
    [SerializeField] private GameObject machinegunEquip;

    void Start()
    {
        
    }

    public void ShootEnemy()
    {
        RaiseArm();
        
        Vector3 forward = GetComponentInParent<NPCMovement>().GetComponent<Transform>().forward;
        
        if (_fireState == FireState.Ready)
        {
            GetComponentInChildren<GunScript_ML>().FireGun(0.7f, forward);
            ShotsFired++;

            if (ShotsFired == 3)
            {
                _fireState = FireState.Fireing;
                StartCoroutine(DelayVolley());
            }
        }

    }

    private IEnumerator DelayVolley()
    {
        yield return new WaitForSeconds(2);
        _fireState = FireState.Ready;
        ShotsFired = 0;
    }
    
    public void RaiseArm()
    {
        if (armState == ArmState.Lowered)
        {
            transform.Rotate(-90, 0, 0);
            armState = ArmState.Raised;
            StartCoroutine(DelayArmLower());
        }
    }

    private IEnumerator DelayArmLower()
    {
        yield return new WaitForSeconds(2);
        transform.Rotate(90, 0, 0);
        armState = ArmState.Lowered;
    }
    
}
