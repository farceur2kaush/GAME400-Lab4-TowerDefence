using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager Instance;
    public List<Enemy> Enemies = new List<Enemy>();

    void Awake()
    {
        Instance = this;
    }
    // Register enemy and add it to the enemies list
    public void RegisterEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        UIManager.Instance.CreateHealthBarForEnemy(enemy);
    }

    // Unregister enemy by removing it from the list
    public void UnRegister(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

    // Gets data out of lists and arrays
    public List<Enemy> GetEnemiesInRange(Vector3 position, float range)
    {
        return Enemies.Where(enemy => Vector3.Distance(position,
        enemy.transform.position) <= range).ToList();
    }

    // detroys all enemies from the enemy list
    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            Destroy(enemy.gameObject);
        }
        Enemies.Clear();
    }

}
