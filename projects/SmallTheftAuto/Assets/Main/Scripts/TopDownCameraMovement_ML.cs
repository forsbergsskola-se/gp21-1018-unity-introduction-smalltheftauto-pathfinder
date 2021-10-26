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

    private float playerStopCounter;
    
    void Start()
    {
        theCamera = GetComponentInChildren<Camera>();
        theCamera.orthographicSize = 2;
        thePlayer = GameObject.FindWithTag("ThePlayer");

        UIHealthbarScript_ML.OnPlayerDeath += PlayerDies;
        PlayerMovement_ML.cameraTracking += PlayerMovement;
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
        if (thePlayer)
        {
            zoomLevel += Input.mouseScrollDelta.y * sensitivity;
            zoomLevel = Mathf.Clamp(zoomLevel, 1, 7);
            theCamera.orthographicSize = zoomLevel;

            if (_playerMoveState == PlayerMoveState.Stopped)
            {
                playerStopCounter += Time.deltaTime + 1;
            }

            if (_playerMoveState == PlayerMoveState.Moving && playerStopCounter > 3)
            {
                playerStopCounter = 0;
                StartCoroutine(DelayMoveCamera());
            }
            
            else if (_cameraState == CameraState.SearchForPlayer)
            {
                Vector3 pos = Vector3.Lerp(transform.position,
                    thePlayer.transform.position + new Vector3(0, 200, 0), Time.deltaTime);
                transform.position = pos;

                if (transform.position == thePlayer.transform.position + new Vector3(0, 200, 0))
                {
                    _cameraState = CameraState.OnPlayer;
                }
            }
            else
            {
                transform.position = thePlayer.transform.position + new Vector3(0, 200, 0);
            }
        }
    }
    
    private IEnumerator DelayStopSearch()
    {
        yield return new WaitForSeconds(1);
        _cameraState = CameraState.OnPlayer;
    }
    
    private IEnumerator DelayMoveCamera()
    {
        yield return new WaitForSeconds(1);
        _cameraState = CameraState.SearchForPlayer;
    }
}
