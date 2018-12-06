using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;               // Singleton pattern reference
    public GameObject addTowerWindow;               // Reference to AddTowerWindow
    public GameObject towerInfoWindow;              // Reference to TowerInfoWindow

    // References to the windows and the background 
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;

    //References to the Text components in the TopBar
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;


    // Singleton
    void Awake()
    {
        Instance = this;
    }

    // Update some text values
    private void UpdateTopBar()
    {
        txtGold.text = GameManager.Instance.gold.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " +
        WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " +
        GameManager.Instance.escapedEnemies + " / " +
        GameManager.Instance.maxAllowedEscapedEnemies;
    }

    // Fetches a towerslot value and pass it aond to AddNewWindow and shows position of the slot using the utility method
    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        addTowerWindow.SetActive(true);
        addTowerWindow.GetComponent<AddTowerWindow>().
        towerSlotToAddTowerTo = towerSlot;
        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateTopBar();
    }

    // sees tower info, opens up towerInfoWindow and moves it to the tower position
    public void ShowTowerInfoWindow(Tower tower)
    {
        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower;
        towerInfoWindow.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.
        GetComponent<RectTransform>(), tower.transform.position);
    }

    // Enables Win Screen
    public void ShowWinScreen()
    {
        blackBackground.SetActive(true);
        winGameWindow.SetActive(true);
    }

    // Enables Lose Screen
    public void ShowLoseScreen()
    {
        blackBackground.SetActive(true);
        loseGameWindow.SetActive(true);
    }
}
