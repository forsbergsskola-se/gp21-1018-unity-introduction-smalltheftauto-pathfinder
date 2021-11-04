using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wheel
{
    public WheelCollider collider;

    public bool steerable;
    public bool powered;
    public bool hasBrakes;
}

public class Vehicle : MonoBehaviour
{
    [SerializeField] private Wheel[] wheels = { };
    [SerializeField] float motorTorque = 1000;
    [SerializeField] float brakeTorque = 2000;
    [SerializeField] float steeringAngle = 45;
    
    public bool insideCar = false;
    private bool canExit = false;
    

    private GameObject storePlayer;
    public delegate void CarExitEvent(GameObject thePLayer);
    public static event CarExitEvent OnExitCar;
    
    private void Start()
    {
        
    }

    public void CarEnter()
    {
        storePlayer = GameObject.FindWithTag("ThePlayer");
        storePlayer.SetActive(false);
        insideCar = true;

        StartCoroutine(DelayExit());
    }

    private void CarExit()
    {
        insideCar = false;
        canExit = false;
        storePlayer.transform.position = transform.position + transform.right * 3;
    
        storePlayer.SetActive(true);

        if (OnExitCar != null)
        {
            OnExitCar(storePlayer);
        }
    }

    private IEnumerator DelayExit()
    {
        yield return new WaitForSeconds(0.3f);
        canExit = true;
    }
    
    void Update()
    {
        if (insideCar)
        {
            var vertical = Input.GetAxis("Vertical");

            float motorTorqueToApply;
            float brakeTorqueToApply = 0;

            if (vertical >= 0)
            {
                motorTorqueToApply = vertical * motorTorque;
                brakeTorqueToApply = 0;
            }
            else if (vertical < 0)
            {
                motorTorqueToApply = vertical * motorTorque;
            }
            
            else
            {
                motorTorqueToApply = 0;
                brakeTorqueToApply = Mathf.Abs(vertical) * brakeTorque;
            }

            var currentSteeringAngle =
                Input.GetAxis("Horizontal") * steeringAngle;

            for (int wheelNum = 0; wheelNum < wheels.Length; wheelNum++)
            {

                var wheel = wheels[wheelNum];

                if (wheel.powered)
                {
                    wheel.collider.motorTorque = motorTorqueToApply;
                }

                if (wheel.steerable)
                {
                    wheel.collider.steerAngle = currentSteeringAngle;
                }

                if (wheel.hasBrakes)
                {
                    wheel.collider.brakeTorque = brakeTorqueToApply;
                }
            }
            
            if (canExit && Input.GetKeyDown(KeyCode.E))
            {
                CarExit();
            }
        }
    }
}
