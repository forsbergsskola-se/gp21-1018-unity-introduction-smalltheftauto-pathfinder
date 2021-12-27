using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: I don't think that you needed four copies of this script.
// And if you did, then please use Inheritance instead of Copy-Paste.
public class PedestrianB : MonoBehaviour
{
    public float moveSpeed;
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(targetTransform != null) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetTransform.transform.position, Time.deltaTime * moveSpeed);
        }
    }
    public void Initialize(Transform target, float moveSpeed) {
        this.targetTransform = target;
        this.moveSpeed = moveSpeed;
    }
}
