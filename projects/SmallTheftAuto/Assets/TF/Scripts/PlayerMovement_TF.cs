using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_TF : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed;
    private Vector3 direction;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Movement();
        
    }

    public void Movement()
    {
        float horizentalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = new Vector3(horizentalInput, 0, verticalInput);
        transform.Translate(direction * speed * Time.deltaTime);

    }   
}
