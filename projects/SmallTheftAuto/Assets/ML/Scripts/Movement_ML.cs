using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_ML : MonoBehaviour
{
    // The speed at which we can move, in units per second.
    [Range(5, 30), SerializeField] float moveSpeed = 6f;

    [Range(0.0f, 10.0f), SerializeField] float acceleration = 0.5f;
    [Range(6.0f, 30.0f), SerializeField] float minSpeed = 6f;
    [Range(6.0f, 300.0f), SerializeField] float maxSpeed = 15f;
    [SerializeField] private float turnSpeed = 120;
    Quaternion bodyStartOrientation;
    Vector3 moveDirection = Vector3.zero;
    
    float yaw = 0f;

    // A cached reference to the character controller, which we'll be
    // using often.
    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        bodyStartOrientation = transform.localRotation;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
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
}