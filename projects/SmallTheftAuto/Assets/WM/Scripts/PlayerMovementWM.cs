using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // We move 5 Units scaled by the Time (per second) scaled by the User Up-Input
        transform.Translate(0f, 5f*Time.deltaTime*Input.GetAxis("Vertical"), 0f);
        // We rotate -180 Degrees scaled by the Time (per second) scaled by the User-Right-Input
        transform.Rotate(0f, 0f, -180f*Time.deltaTime*Input.GetAxis("Horizontal"));
    }
}
