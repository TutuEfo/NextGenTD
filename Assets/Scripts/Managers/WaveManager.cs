using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawnerTemp spawner;
    public int currentWave = 0;
    public int enemiesPerWave = 7;
    public int maxWaves = 10;
    public float spawnInterval = 0.8f;

    private int enemiesSpawned = 0;
    private int enemiesAlive = 0;
    private bool isSpawning = false;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (isSpawning || currentWave >= maxWaves) return;

        currentWave++;
        enemiesSpawned = 0;
        isSpawning = true;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int total = enemiesPerWave * currentWave;

        for (int i = 0; i < total; i++)
        {
            spawner.SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        TryFinishWave();
    }

    public void NotifyEnemySpawned()
    {
        enemiesAlive++;
    }

    public void OnEnemyRemoved()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        TryFinishWave();
    }

    private void TryFinishWave()
    {
        if (!isSpawning && enemiesAlive <= 0)
        {
            GameManager.Instance.WaveCompleted();

            if (currentWave < maxWaves)
            {
                StartNextWave();
            }
        }
    }
}