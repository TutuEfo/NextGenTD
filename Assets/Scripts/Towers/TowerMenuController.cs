using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerMenuController : MonoBehaviour
{
    public RectTransform root;
    public Transform buttonContainer;
    public TowerButton buttonPrefab;

    BuildSpot currentSpot;

    public void OpenFor(BuildSpot spot, TowerDefinition[] catalog, Vector3 worldPos)
    {
        currentSpot = spot;

        foreach (Transform child in buttonContainer) Destroy(child.gameObject);

        foreach (var def in catalog)
        {
            var btn = Instantiate(buttonPrefab, buttonContainer);
            btn.Bind(def, TryPick);
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        root.position = screenPos;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        currentSpot = null;
        gameObject.SetActive(false);
    }

    void TryPick(TowerDefinition def)
    {
        if (currentSpot == null) return;

        var ok = TowerPlacer.Instance.TryPlace(def, currentSpot.GetBuildWorldPos());

        if (ok)
        {
            currentSpot.MarkOccupied(true);
            Close();
        }
    }
}
