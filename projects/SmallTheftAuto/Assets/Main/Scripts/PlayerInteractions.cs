using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private int layerMask = 0;
    
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 7, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, layerMask))
        {
                
            if (hit.collider.CompareTag("Car"))
            {
                Debug.Log("Car hit");
            }
            

          
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }

    private void EnterCar()
    {
        
    }
}
