using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTowerWindow : MonoBehaviour {

    public GameObject towerSlotToAddTowerTo;                    // Coordinates to the tower slot

    // take tower type as parameter
    public void AddTower(string towerTypeAsString)
    {
        // Convert enum type to string so that we can use that for event triggers
        Tower.TowerType type = (Tower.TowerType)Enum.Parse(typeof(Tower.TowerType), towerTypeAsString, true);

        // Checks if the player has required Gold or not
        if (TowerManager.Instance.GetTowerPrice(type) <= GameManager.Instance.gold)
        {
            // Subtract the tower price from the total gold
            GameManager.Instance.gold -= TowerManager.Instance.GetTowerPrice(type);
  
            // call CreateNewTower and disable the AddTowerWindow
            TowerManager.Instance.CreateNewTower(towerSlotToAddTowerTo, type);
            gameObject.SetActive(false);
        }
    }
}
