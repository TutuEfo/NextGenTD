using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSpot : MonoBehaviour, IPointerClickHandler
{
    public TowerDefinition[] catalog;
    public bool occupied = false;
    public Vector3 placementOffset;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (occupied) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        var menu = FindObjectOfType<TowerMenuController>(true);
        menu.OpenFor(this, catalog, transform.position);
    }

    public Vector3 GetBuildWorldPos() => transform.position + placementOffset;
    public void MarkOccupied(bool value) => occupied = value;
}