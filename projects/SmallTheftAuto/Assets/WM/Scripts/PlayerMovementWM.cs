using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWM : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    
    private Rigidbody rigidBody;
    private KeyCode[] inputKeys;
    private Vector3[] directionsForKeys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 5f*Time.deltaTime*Input.GetAxis("Vertical"), 0f);
        transform.Rotate(0f, 0f, -180f*Time.deltaTime*Input.GetAxis("Horizontal"));
    }
}
