using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement_ML : MonoBehaviour
{
    
    private static float  zoomLevel = 10;
    
    float yaw = 0f;
    float pitch = 0f;
    private float turnSpeed = 20;
    public float zoomPosition;
    
    public float sensitivity=1;
    public float maxZoom=30;

    [SerializeField] private float speed = 20;

    private GameObject thePlayer;
    private Camera theCamera;
    Quaternion bodyStartOrientation;
    
    void Start()
    {
        theCamera = GetComponentInChildren<Camera>();
        theCamera.orthographicSize = 2;
        thePlayer = GameObject.FindWithTag("ThePlayer");

        UIHealthbarScript_ML.OnPlayerDeath += PlayerDies;
    }

    private void PlayerDies()
    {
     //   Destroy(gameObject);
    }

    private IEnumerator Delay()
    {
        yield return new WaitUntil(() => thePlayer != null);
        transform.position = thePlayer.transform.position;
    }
    
    void Update()
    {
        if (thePlayer)
        {
            zoomLevel += Input.mouseScrollDelta.y * sensitivity;
            zoomLevel = Mathf.Clamp(zoomLevel, 1, 7);
            theCamera.orthographicSize = zoomLevel;

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var offset = new Vector3(horizontal, 0, vertical)
                         * Time.deltaTime * speed;

            transform.position = thePlayer.transform.position + new Vector3(0, 200, 0);
        }
    }
}