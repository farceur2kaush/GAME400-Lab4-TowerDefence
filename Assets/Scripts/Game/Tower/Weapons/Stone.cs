using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : FollowingProjectile {

    public float damage;        // damage inflicted
   
    // method override
    protected override void OnHitEnemy()
    {
        // damage dealt to the enemy and projectile destroyed
        enemyToFollow.TakeDamage(damage);
        Destroy(gameObject);
    }

}
