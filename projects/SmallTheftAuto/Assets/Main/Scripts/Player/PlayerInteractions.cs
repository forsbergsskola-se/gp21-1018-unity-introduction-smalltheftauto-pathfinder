using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public delegate void CarEnterEvent(GameObject carToEnter);
    public static event CarEnterEvent OnEnterCar;

/// <summary>
///
/// I think this looks nice but could be useful to have the OnEnterCar method in this script. It'd probably also be good to replace null with a different approach
/// </summary>
/// <param name="carToEnter"></param>
    private void EnterCar(GameObject carToEnter)
    {
        if (OnEnterCar != null)
        {
            OnEnterCar(carToEnter);
        }
    }
    
    void Update()
    {
        Ray newRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        Debug.DrawLine(transform.position, transform.forward * 1);
        int layerMask = 1 << 7;
    //    layerMask = ~layerMask;


        if (Physics.Raycast(newRay, out hit, 1))
        {
            if (hit.collider.CompareTag("Car"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnterCar(hit.collider.gameObject);

                    hit.collider.gameObject.GetComponent<Vehicle>().CarEnter();
                }
            }
            
        }
    }
    
}
