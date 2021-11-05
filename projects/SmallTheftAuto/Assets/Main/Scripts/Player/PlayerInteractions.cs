using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public delegate void CarEnterEvent(GameObject carToEnter);
    public static event CarEnterEvent OnEnterCar;


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
                Debug.Log(hit.collider.name);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnterCar(hit.collider.gameObject);

                    hit.collider.gameObject.GetComponent<Vehicle>().CarEnter();
                }
            }
            
        }
    }
    
}
