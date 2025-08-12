using UnityEngine;

public class EnemySpawnerTemp : MonoBehaviour
{
    public GameObject enemyPrefab;
    public EnemyPath path;
    public WaveManager waveManager;

    public void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab, path.waypoints[0].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().SetPath(path.waypoints);
        waveManager?.NotifyEnemySpawned();
    }
}