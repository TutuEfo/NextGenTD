using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button upgradeButton;
    private Tower selectedTower;

    private void Start()
    {
        upgradePanel.SetActive(false);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    private void OnUpgradeButtonClicked()
    {
        if (selectedTower != null)
        {
            bool upgraded = selectedTower.Upgrade();

            if (!upgraded)
            {
                Debug.Log("Upgrade Failed");
            }
        }
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;

        if (selectedTower != null)
        {
            upgradePanel.SetActive(true);
        }
        else
        {
            upgradePanel.SetActive(false);
        }
    }

    public void DeselectTower()
    {
        selectedTower = null;
        upgradePanel.SetActive(false);
    }
}
