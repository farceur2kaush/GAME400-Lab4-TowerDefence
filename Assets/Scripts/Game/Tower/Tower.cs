using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public enum TowerType
    {
        Stone, Fire, Ice
    }

    public float attackPower = 3f;                      // Damage inflicted
    public float timeBetweenAttacksInSeconds = 1f;      // Cooldown 
    public float aggroRadius = 15f;                     // tower enemy detection range
    public int towerLevel = 1;
    public TowerType type;
    public AudioClip shootSound;                        // Tower shoot sound
    public Transform towerPieceToAim;                   // tranform to be aimed at enemy head
    public Enemy targetEnemy = null;                    // Current target
    private float attackCounter;                        // Countdown till the shooting begins

    private void SmoothlyLookAtTarget(Vector3 target)
    {
        towerPieceToAim.localRotation = UtilityMethods.SmoothlyLook(towerPieceToAim, target);
    }
    protected virtual void AttackEnemy()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound, .15f);
    }

    // Return the enemy withinshooting radius
    public List<Enemy> GetEnemiesInAggroRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        // Iterate through the enemy list and return the one within the radius
        foreach (Enemy enemy in EnemyManager.Instance.Enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= aggroRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    // Returns the closest enemy in range
    public Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        
        //  Iterate thorugh the list of enemies that are in the range and returns the closest one among them 
        foreach (Enemy enemy in GetEnemiesInAggroRange())
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < smallestDistance)
            {
                smallestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // decrement the attackcounter with time
        attackCounter -= Time.deltaTime;

        // check if an enemy is targeted or not
        if (targetEnemy == null)
        {
            // if there is a need to rotate then look at the tower idle position
            if (towerPieceToAim)
            {
                SmoothlyLookAtTarget(towerPieceToAim.transform.position - new Vector3(0, 0, 1));
            }

            // try to look for a new target enemy
            if (GetNearestEnemyInRange() != null && Vector3.Distance
            (transform.position, GetNearestEnemyInRange().transform.position) <= aggroRadius)
            {
                targetEnemy = GetNearestEnemyInRange();
            }
        }
        else
        { 
            // check if there's a target enemy assigned if yes then look at the target
            if (towerPieceToAim)
            {
                SmoothlyLookAtTarget(targetEnemy.transform.position);
            }

            if (attackCounter <= 0f)
            {
                // Attack
                AttackEnemy();
                // Reset attack counter
                attackCounter = timeBetweenAttacksInSeconds;
            }
            // if enemy out of range, then set the target to null
            if (Vector3.Distance(transform.position,
            targetEnemy.transform.position) > aggroRadius)
            {
                targetEnemy = null;
            }
        }
    }

    public void LevelUp()
    {
        towerLevel++;
        //Calculate new stats for this tower
        attackPower *= 2;
        timeBetweenAttacksInSeconds *= 0.7f;
        aggroRadius *= 1.20f;
    }
}
