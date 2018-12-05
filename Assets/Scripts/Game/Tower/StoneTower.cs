using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTower : Tower
{

    public GameObject stonePrefab;                                  // projectile shot by tower

    // override attack enemy after calling the base one first
    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        // Spwan a new stone projectile
        GameObject stone = (GameObject)Instantiate(stonePrefab, towerPieceToAim.position, Quaternion.identity);
        stone.GetComponent<Stone>().enemyToFollow = targetEnemy;
        stone.GetComponent<Stone>().damage = attackPower;
    }

}
