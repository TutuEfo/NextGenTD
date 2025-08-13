using UnityEngine;

public class EnemySpawnerTemp : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject normalPrefab;
    public GameObject sprinterPrefab;

    [Header("References")]
    public EnemyPath path;
    public WaveManager waveManager;

    public void SpawnNormal() => SpawnEnemy(normalPrefab);
    public void SpawnSprinter() => SpawnEnemy(sprinterPrefab);

    public void SpawnEnemy(GameObject prefab)
    {
        if (prefab != null || path != null || path.waypoints.Length == 0)
        {
            return;
        }

        var enemy = Instantiate(prefab, path.waypoints[0].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>()?.SetPath(path.waypoints);
        waveManager?.NotifyEnemySpawned();
    }
}