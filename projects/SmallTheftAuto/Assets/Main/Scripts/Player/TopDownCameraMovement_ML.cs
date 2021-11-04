using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveState
{
    Stopped,
    Moving
}



public enum CameraState
{
    OnPlayer,
    DelaySearch,
    SearchForPlayer
}

public class TopDownCameraMovement_ML : MonoBehaviour
{
    private CameraState _cameraState;
    private PlayerMoveState _playerMoveState;
    private static float  zoomLevel = 10;
    private float distancePlayer = 20f;
    float yaw = 0f;
    float pitch = 0f;
    private float turnSpeed = 20;
    public float zoomPosition;
    private Vector3 cameraPos;
    private float x, y, z; 
    public float sensitivity=1;
    public float maxZoom=30;

    [SerializeField] private float speed = 20;

    private GameObject followObject;
    private Camera theCamera;
    Quaternion bodyStartOrientation;

    private float playerStopCounter;

    private float followDelay = 1; 
    
    void Start()
    {
        theCamera = GetComponentInChildren<Camera>();
   //     theCamera.orthographicSize = 2;
        followObject = GameObject.FindWithTag("ThePlayer");
        y = distancePlayer;
        
        UIHealthbarScript_ML.OnPlayerDeath += PlayerDies;
        PlayerMovement_ML.cameraTracking += PlayerMovement;
        PlayerInteractions.OnEnterCar += EnterCar;
        Vehicle.OnExitCar += ExitCar;
    }

    private void ExitCar(GameObject objectToFollow)
    {
        followDelay = 1;
        followObject = objectToFollow;
    }
    
    private void EnterCar(GameObject objectToFollow)
    {
        followDelay = 0.4f;
        followObject = objectToFollow;
    }

    private void PlayerMovement(PlayerMoveState playerMoveState)
    {
        _playerMoveState = playerMoveState;
    }
    
    private void PlayerDies()
    {
     //   Destroy(gameObject);
    }

    void Update()
    {
        if (followObject)
        {
        //    zoomLevel += Input.mouseScrollDelta.y * sensitivity;
        //    zoomLevel = Mathf.Clamp(zoomLevel, 1, 7);
        //    theCamera.orthographicSize = zoomLevel;
            x = followObject.transform.position.x;
            z = followObject.transform.position.z;
            cameraPos.Set(x,y, z);    
        
            if (_playerMoveState == PlayerMoveState.Stopped)
            {
                Debug.Log("Player stopped");
                playerStopCounter += Time.deltaTime + 1;
            }
            

            if (_playerMoveState == PlayerMoveState.Moving && playerStopCounter > 3)
            {
                playerStopCounter = 0;
                StartCoroutine(DelayMoveCamera());
            }


            else if (_cameraState == CameraState.SearchForPlayer)
            {
                transform.position  = Vector3.Lerp(transform.position,
                    followObject.transform.position + new Vector3(0, distancePlayer, 0), Time.deltaTime);

                if (transform.position == followObject.transform.position + new Vector3(0, distancePlayer, 0))
                {
                    _cameraState = CameraState.OnPlayer;
                }
            }

            else
            {
                transform.position = cameraPos;
            }
           
        }
    }
    

    private IEnumerator DelayMoveCamera()
    {
        yield return new WaitForSeconds(followDelay);
        _cameraState = CameraState.SearchForPlayer;
    }
}
