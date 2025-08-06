using System;
using UnityEngine;

public class EnemySpawnerTemp : MonoBehaviour
{
    public GameObject enemyPrefab;
    public EnemyPath path;

    public float spawnRate = 2f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            GameObject enemy = Instantiate(enemyPrefab, path.waypoints[0].position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().SetPath((path.waypoints));
            timer = 0f;
        }
    }
}
