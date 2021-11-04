using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Collections;
using UnityEngine;


public class PlayerMovement_ML : MonoBehaviour
{
 
    [Range(5, 30), SerializeField] float moveSpeed = 6f;

    [Range(0.0f, 10.0f), SerializeField] float acceleration = 0.5f;
    [Range(6.0f, 30.0f), SerializeField] float minSpeed = 6f;
    [Range(6.0f, 300.0f), SerializeField] float maxSpeed = 15f;
    [SerializeField] private float turnSpeed = 120;
    Quaternion bodyStartOrientation;
    Vector3 moveDirection = Vector3.zero;

    
    Vector3 velocity;      //TF
    bool isGrounded;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;






    public delegate void CameraTrackingEvent(PlayerMoveState playerMoveState);


    public static event CameraTrackingEvent cameraTracking;


    private void OnCameraTracking(PlayerMoveState playerMoveState)
    {
        if (cameraTracking != null)
        {
            cameraTracking(playerMoveState);
        }
    }
    
    public static Transform PlayerTransform { get; private set; }
    
    float yaw = 0f;
    
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        bodyStartOrientation = transform.localRotation;

   //     Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    
    }

 
    
    void Update()
    {
        //TF
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        PlayerTransform = transform;
        
        var horizontal = Input.GetAxis("Mouse X")
                         * Time.deltaTime * turnSpeed;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            OnCameraTracking(PlayerMoveState.Moving);
        }
        else
        {
            OnCameraTracking(PlayerMoveState.Stopped);
        }

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

        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(move * moveSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

    }
    
   
}