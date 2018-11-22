using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour {

    // Refrence to this script
    public static WayPointManager Instance;
    
    // List that stores the waypoints
    public List<Path> Paths = new List<Path>();
    void Awake()
    {
        // Singleton Pattern - makes sure only one instance of this is generated
        Instance = this;
    }
    // Return position of waypoint where the enemy is spawned
    public Vector3 GetSpawnPosition(int pathIndex)
    {
        return Paths[pathIndex].WayPoints[0].position;
    }
}

[System.Serializable]
public class Path
{
    public List<Transform> WayPoints = new List<Transform>();
}

