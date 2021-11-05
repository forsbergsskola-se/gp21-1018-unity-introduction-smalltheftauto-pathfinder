using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateMeshRenderer : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
