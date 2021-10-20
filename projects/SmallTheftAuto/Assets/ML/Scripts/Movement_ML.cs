using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Collections;
using UnityEngine;

enum ArmState
{
    Lowered,
    Raised
}

public class Movement_ML : MonoBehaviour
{
    private ArmState theArmState;
    [Range(5, 30), SerializeField] float moveSpeed = 6f;

    [Range(0.0f, 10.0f), SerializeField] float acceleration = 0.5f;
    [Range(6.0f, 30.0f), SerializeField] float minSpeed = 6f;
    [Range(6.0f, 300.0f), SerializeField] float maxSpeed = 15f;
    [SerializeField] private float turnSpeed = 120;
    Quaternion bodyStartOrientation;
    Vector3 moveDirection = Vector3.zero;
    private GameObject arm = null;
    
    public delegate void GunFiredEventHandler(Vector3 forwardVector);
    public static event GunFiredEventHandler PlayerGunFired;

    protected virtual void OnGunFired(Vector3 forwardVector)
    {
        if (PlayerGunFired != null)
        {
            PlayerGunFired(forwardVector);
        }
    }
    
    float yaw = 0f;
    
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        bodyStartOrientation = transform.localRotation;
        

        arm = GameObject.FindWithTag("Arm");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Mouse X")
                         * Time.deltaTime * turnSpeed;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * 0) + (transform.forward * z);

        bool Shift = Input.GetKey(KeyCode.LeftShift);
        
        
        var horizontalMouse = Input.GetAxis("Mouse X")
                              * Time.deltaTime * turnSpeed;
        
    
        yaw += x;
        
        var bodyRotation = Quaternion.AngleAxis(yaw, Vector3.up);

   
        transform.localRotation = bodyRotation * bodyStartOrientation;


        if (Input.GetButtonDown("Fire1"))
        {
            if (arm != null && theArmState == ArmState.Lowered)
            {
                arm.transform.Rotate(-90, 0, 0);
                theArmState = ArmState.Raised;
                StartCoroutine("Delay");
            }
            OnGunFired(transform.forward);
        }
        
        if(Shift)
        {
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += acceleration * Time.deltaTime;
            }
            
        }
       
        if (!Shift)
        {
            if (moveSpeed > minSpeed)
            {
                moveSpeed -= (acceleration * 1.5f) * Time.deltaTime;
            }
        }
        
        controller.Move(move * moveSpeed * Time.deltaTime);

    }
    
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        arm.transform.Rotate(90, 0, 0);
        theArmState = ArmState.Lowered;
    }
}