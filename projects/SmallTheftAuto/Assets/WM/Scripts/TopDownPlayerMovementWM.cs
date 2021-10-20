using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovementWM : MonoBehaviour
{
    public float moveSpeed;
    public GameObject player;

    private Transform rigTransform;

    // Start is called before the first frame update
    void Start()
    {
        rigTransform = this.transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(player == null){
            return;
        }

        rigTransform.position = Vector3.Lerp(rigTransform.position, player.transform.position, 
            Time.deltaTime * moveSpeed);
    }

}
