using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWM : MonoBehaviour
{
    public GameObject player;
    public CarMovementWM carMovementWM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.F)) {
            if (this.player.activeInHierarchy) {
                this.player.SetActive(false);
                this.carMovementWM.enabled = true;
            }
        }
        
    }
}
