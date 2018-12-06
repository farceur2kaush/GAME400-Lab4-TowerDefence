using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct TowerCost
{
    public Tower.TowerType TowerType;
    public int Cost;
}

/// <summary>
/// Had to user tower. in order for some things to work in the class
/// </summary>
public class TowerManager : MonoBehaviour {

    public static TowerManager Instance;                            // Singleton Reference

    public GameObject stoneTowerPrefab;                             // References to stonePrefab, to spawn instances of this
    public GameObject fireTowerPrefab;
    public GameObject iceTowerPrefab;
    
    public List<TowerCost> TowerCosts = new List<TowerCost>();      // stores the Cost of each type of tower in a list
    
    // Sets Instance variable
    void Awake()
    {
        Instance = this;
    }

    // cerate a new cope of the type of tower passed in parameters and also place it on the mentioned slot
    // Also disables the slot so we dont' place two towers in one spot
    public void CreateNewTower(GameObject slotToFill, Tower.TowerType towerType)
    {
        switch (towerType)
        {
            case Tower.TowerType.Stone:
                Instantiate(stoneTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;

            case Tower.TowerType.Fire:
                Instantiate(fireTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;

            case Tower.TowerType.Ice:
                Instantiate(iceTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
        }
    }
    // LINQ utility method to access the price of tower type
    public int GetTowerPrice(Tower.TowerType towerType)
    {
        return (from towerCost in TowerCosts where towerCost.TowerType == towerType
                select towerCost.Cost).FirstOrDefault();
    }
}
