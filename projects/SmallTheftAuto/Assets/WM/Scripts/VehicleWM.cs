using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWM : MonoBehaviour
{
    public GameObject player;

    public CarMovementWM carMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        carMovement = GetComponent<CarMovementWM>();

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                
            }
        }
    }
}
