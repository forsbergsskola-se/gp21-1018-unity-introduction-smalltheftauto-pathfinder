using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour
{
    
    private static float  zoomLevel = Mathf.Clamp(zoomLevel, 0, 30);
    public float zoomPosition;
    
    [SerializeField] private float speed = 20;
    // Start is called before the first frame update
    
    // The lower-left position of the camera, on its current X-Z plane.
    [SerializeField] Vector2 minimumLimit = -Vector2.one;

    // The upper-right position of the camera, on its current X-Z plane.
    [SerializeField] Vector2 maximumLimit = Vector2.one;

    [SerializeField]  private GameObject thePlayer;
    
    [SerializeField] 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        
        // Compute how much movement to apply this frame, in world space
        var offset = new Vector3(horizontal, 0, vertical)
                     * Time.deltaTime * speed;
        
        var newPosition = transform.position + offset;
        transform.position = thePlayer.transform.position + new Vector3(0,200,0);
        
   //     zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, 30 * Time.deltaTime);
   //     transform.position = transform.position + (transform.forward * zoomPosition);
        

    }

    // Computes the bounding box that the camera is allowed to be in.
    Bounds bounds 
    {
        get 
        {

            // We'll create a bounding box that's zero units high, and
            // positioned at the 
            
            //current height of the camera.
                var cameraHeight = transform.position.y;

            // Figure out the position of the corners of the boxes in
            // world space
            Vector3 minLimit = new Vector3(minimumLimit.x,
                cameraHeight, minimumLimit.y);
            Vector3 maxLimit = new Vector3(maximumLimit.x,
                cameraHeight, maximumLimit.y);
            // Create a new Bounds using these values and return it
            var newBounds = new Bounds();
            newBounds.min = minLimit;
            newBounds.max = maxLimit;
            return newBounds;
        }
    }


    // Draw the bounding box.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

}
