using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    
    public Vector3 offset;

    private void LateUpdate()
    {

        //Vector3 desiredPos = 

        //Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal") * -1.1f));
        transform.position = target.position + offset;

        

        

    }
}
