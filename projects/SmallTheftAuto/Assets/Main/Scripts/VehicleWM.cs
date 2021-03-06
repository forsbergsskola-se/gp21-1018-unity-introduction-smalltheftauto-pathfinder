using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWM : MonoBehaviour
{
    public GameObject player;
    public CarMovementWM carMovementWM;
    public TopDownPlayerCameraWM topDownPlayerCameraWM;

    public TopDownCarCameraWM topDownCarCameraWM;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (this.player.activeInHierarchy)
            {
                if (PlayerIsCloseToCar())
                {
                    EnterCar();
                }
            }
        }

        if (Input.GetKey(KeyCode.G))
        {
            if (!this.player.activeInHierarchy)
            {
                LeaveCar();
            }
        }
    }
    

    bool PlayerIsCloseToCar()
    {
        return Vector3.Distance(
            this.player.transform.position,
            this.transform.position) < 2;
    }

    public void EnterCar()
    {
        this.player.SetActive(false);
        this.carMovementWM.enabled = true;
        this.topDownPlayerCameraWM.enabled = false;
        this.topDownCarCameraWM.enabled = true;
    }

    public void LeaveCar()
    {
        this.player.transform.position = this.transform.position;
        this.player.SetActive(true);
        this.carMovementWM.enabled = false;
        this.topDownPlayerCameraWM.enabled = true;
        this.topDownCarCameraWM.enabled = false;
    }
}