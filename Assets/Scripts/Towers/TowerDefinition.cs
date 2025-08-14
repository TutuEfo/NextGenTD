using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "TD/Tower Definition")]
public class TowerDefinition : ScriptableObject
{
    public string towerName;
    public Sprite icon;
    public int cost;
    public GameObject prefab;
}
