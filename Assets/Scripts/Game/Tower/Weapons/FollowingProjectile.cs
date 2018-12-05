using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowingProjectile : MonoBehaviour {

    public Enemy enemyToFollow;                                 // Target Enemy
    public float moveSpeed = 15;                                // Projectile speed in units/sec

    private void Update()
    {
        // if the enemy the projectile was follwing doesn't exist anymore then projectile should destroy itself
        if (enemyToFollow == null)
        {
            Destroy(gameObject);
        }
        else
        { // as long as there's a target, look at it and move towards it
            transform.LookAt(enemyToFollow.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
        }
    }

    // if projectile hits the enemy then call OnHitEnemy
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() == enemyToFollow)
        {
            OnHitEnemy();
        }
    }
    //6
    protected abstract void OnHitEnemy();       // an abstract method which works similar to private but this one can also be called from the derived class
}
