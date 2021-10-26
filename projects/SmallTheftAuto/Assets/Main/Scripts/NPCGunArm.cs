using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGunArm : MonoBehaviour
{
    // Start is called before the first frame update
    public ArmState armState;
//    [SerializeField] GameObject 
    
    
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
