using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour
{
    
    private static float  zoomLevel = Mathf.Clamp(zoomLevel, 0, 30);
    public float zoomPosition;
    
    [SerializeField] private float speed = 20;

    [SerializeField] Vector2 minimumLimit = -Vector2.one;

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
        
   
        var offset = new Vector3(horizontal, 0, vertical)
                     * Time.deltaTime * speed;
        
        var newPosition = transform.position + offset;
        transform.position = thePlayer.transform.position + new Vector3(0,200,0);
        
   //     zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, 30 * Time.deltaTime);
   //     transform.position = transform.position + (transform.forward * zoomPosition);
        

    }


}
