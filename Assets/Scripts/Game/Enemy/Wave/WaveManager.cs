using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    public static WaveManager Instance;
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    private float elapsedTime = 0f;
    private EnemyWave activeWave;
    private float spawnCounter = 0f;
    // to track the started waves, so that is doesn't start again
    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        SearchForWave();
        UpdateActiveWave();
    }

    private void SearchForWave()
    {
        //goes thorugh enemywave list
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            // Checks if wave is active or not 
            if (!activatedWaves.Contains(enemyWave)
            && enemyWave.startSpawnTimeInSeconds <= elapsedTime)
            {
                // if yes then make the enemy wave the active one and add it to the list of already activated one
                activeWave = enemyWave;
                activatedWaves.Add(enemyWave);
                spawnCounter = 0f;
                GameManager.Instance.waveNumber++;

                break;
            }
        }
    }
    
    private void UpdateActiveWave()
    {
        // countinue if there's an active wave
        if (activeWave != null)
        {
            spawnCounter += Time.deltaTime;

            // if spawnCounter is higher than the active wave's timeBetweenSpawnsInSeconds, 
            // then there's an enemy that needs to be spawned
            if (spawnCounter >= activeWave.timeBetweenSpawnsInSeconds)
            {
                spawnCounter = 0f;

                // Checks for active enemies
                if (activeWave.listOfEnemies.Count != 0)
                {
                    // First enemy goes to the first point of waypoint of the wave's path
                    GameObject enemy = (GameObject)Instantiate(
                    activeWave.listOfEnemies[0], WayPointManager.Instance.
                    GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);
                    
                    // increment enmey index
                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;
                    
                    // remove first entry in the list of enemies
                    activeWave.listOfEnemies.RemoveAt(0);
                }
                else
                {
                    activeWave = null;

                    // Condition to check if all waves are over
                    if (activatedWaves.Count == enemyWaves.Count)
                    {
                        GameManager.Instance.enemySpawningOver = true;
                    }
                }
            }
        }
    }

    public void StopSpawning()
    {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }

}
