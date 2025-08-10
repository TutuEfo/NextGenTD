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
            if (!upgraded) Debug.Log("Upgrade failed (max level or not enough gold).");
        }
        else
        {
            Debug.Log("No tower selected to upgrade.");
        }
    }

    public void SelectTower(Tower tower)
    {
        if (selectedTower == tower) return;
        selectedTower = tower;
        upgradePanel.SetActive(true);
    }

    public void DeselectTower()
    {
        selectedTower = null;
        upgradePanel.SetActive(false);
    }
}