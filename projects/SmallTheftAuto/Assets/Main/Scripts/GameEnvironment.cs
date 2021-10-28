using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> wayPoints = new List<GameObject>();
    public List<GameObject> WayPoints { get { return wayPoints;} }

    public static GameEnvironment SingleTon
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.WayPoints.AddRange(
                    GameObject.FindGameObjectsWithTag("wp"));
                instance.wayPoints = instance.wayPoints.OrderBy(waypoint => waypoint.name).ToList();
            }

            return instance;
        }
    }
}
