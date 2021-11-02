using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdjustCenterOfMass : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass = Vector3.zero;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass += centerOfMass;
    }

    private void OnDrawGizmosSelected()
    {
     
        Gizmos.color = Color.green;

        var currentCenterOfMass =
            this.GetComponent<Rigidbody>().worldCenterOfMass;

        Gizmos.DrawSphere(currentCenterOfMass + centerOfMass, 0.125f);
    }
}
    

