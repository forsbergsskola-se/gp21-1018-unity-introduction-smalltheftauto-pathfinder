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
    
    
    void Update()
    {
        if (insideCar)
        {
            var vertical = Input.GetAxis("Vertical");

            float motorTorqueToApply;
            float brakeTorqueToApply;

            if (vertical >= 0)
            {
                motorTorqueToApply = vertical * motorTorque;
                brakeTorqueToApply = 0;
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
        }
    }
}
