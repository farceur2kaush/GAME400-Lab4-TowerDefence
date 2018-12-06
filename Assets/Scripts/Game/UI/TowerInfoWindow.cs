using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoWindow : MonoBehaviour {

    public Tower tower;                 // Reference to the tower that can be upgraded

    public Text txtInfo;                // Text Component for the info and the text on btn
    public Text txtUpgradeCost;         

    private int upgradePrice;           // Cost to upgrade the tower 
    
    private GameObject btnUpgrade;      // Reference to the upgrade btn

    // Find upgrade btn
    void Awake()
    {
        btnUpgrade = txtUpgradeCost.transform.parent.gameObject;
    }

    // when the windows opens, call UpdateInfo 
    void OnEnable()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        // Calculate new price for upgrade
        upgradePrice = Mathf.CeilToInt(TowerManager.Instance.
        GetTowerPrice(tower.type) * 1.5f * tower.towerLevel);
        
        // Set text to show current tower level
        txtInfo.text = tower.type + " Tower Lv " + tower.towerLevel;
        
        // If level is less than 3, then show upgrade btn, if not then hide it
        if (tower.towerLevel < 3)
        {
            btnUpgrade.SetActive(true);

            txtUpgradeCost.text = "Upgrade\n" + upgradePrice + " Gold";
        }
        else
        {
            btnUpgrade.SetActive(false);
        }
    }
    //6
    public void UpgradeTower()
    {
        if (GameManager.Instance.gold >= upgradePrice)
        {
            GameManager.Instance.gold -= upgradePrice;
            tower.LevelUp();
            gameObject.SetActive(false);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
