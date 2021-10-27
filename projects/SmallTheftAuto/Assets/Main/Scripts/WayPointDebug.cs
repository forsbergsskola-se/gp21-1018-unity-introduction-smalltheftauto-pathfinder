using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class WayPointDebug : MonoBehaviour
{
    void RenameWPs(GameObject overlook)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("wp");

        int i = 1;
        foreach (var el in gos)
        {
            if (el != overlook)
            {
                el.name = "WP" + string.Format("{0:000}", 1);
                i++;
            }
        }
    }

    private void OnDestroy()
    {
        RenameWPs(gameObject);
    }

    void Start()
    {
        if (transform.parent.gameObject.name != "Waypoint") return;
        RenameWPs(null);
    }

    void Update()
    {
        GetComponent<TextMesh>().text = transform.parent.gameObject.name;
    }
}
