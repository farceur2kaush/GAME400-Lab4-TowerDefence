using System;
using System.Collections.Generic;
using UnityEngine;

//1 Makes the class editable in editor 
[Serializable]
public class EnemyWave
{
    public int pathIndex;
    public float startSpawnTimeInSeconds;
    public float timeBetweenSpawnsInSeconds = 1f;
    // List of enemies in a wave
    public List<GameObject> listOfEnemies = new List<GameObject>();
}