using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // The speed at which we can move, in units per second.
    [Range(5, 30), SerializeField] float moveSpeed = 6f;
    
    [Range(0.0f, 10.0f),SerializeField] float acceleration = 0.5f;
    [Range(6.0f, 30.0f), SerializeField] float minSpeed = 6f;
    [Range(6.0f, 300.0f), SerializeField] float maxSpeed = 15f;

    
    Vector3 moveDirection = Vector3.zero;

    // A cached reference to the character controller, which we'll be
    // using often.
    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        bool Shift = Input.GetKey(KeyCode.LeftShift);
        
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