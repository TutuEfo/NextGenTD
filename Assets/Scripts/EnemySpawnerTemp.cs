using System;
using UnityEngine;

public class EnemySpawnerTemp : MonoBehaviour
{
    public GameObject enemyPrefab;
    public EnemyPath path;

    public void SpawnEnemy()
    {
        if (path == null || path.waypoints == null || path.waypoints.Length == 0)
        {
            Debug.LogWarning("Enemy path or waypoints not set!");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, path.waypoints[0].position, Quaternion.identity);
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement != null)
        {
            movement.SetPath(path.waypoints);
        }
    }
}
