using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer Instance;
    public int gold = 100;

    void Awake() => Instance = this;

    public bool CanAfford(TowerDefinition def) => gold >= def.cost;

    public bool TryPlace(TowerDefinition def, Vector3 worldPos)
    {
        if (!CanAfford(def) || def.prefab == null) return false;
        Instantiate(def.prefab, worldPos, Quaternion.identity);
        gold -= def.cost;
        return true;
    }
}
