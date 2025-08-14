using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text label;
    public Button button;

    private TowerDefinition def;
    private System.Action<TowerDefinition> onPick;

    public void Bind(TowerDefinition def, System.Action<TowerDefinition> onPick)
    {
        this.def = def;
        this.onPick = onPick;

        if (icon)
        {
            icon.sprite = def.icon;
        }

        if (label)
        {
            label.text = $"{def.towerName}\n{def.cost}g";
        }

        bool can = TowerPlacer.Instance == null || TowerPlacer.Instance.CanAfford(def);
        
        if (button)
        {
            button.interactable = can;
        }

        if (button)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => this.onPick?.Invoke(this.def));
        }
    }
}