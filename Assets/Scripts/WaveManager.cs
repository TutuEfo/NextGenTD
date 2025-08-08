using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawnerTemp spawner;
    public int currentWave = 0;
    public int enemiesPerWave = 7;
    public int maxWaves = 10;

    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;

    private bool waveActivate = false;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (waveActivate || currentWave >= maxWaves)
        {
            return;
        }

        currentWave++;
        waveActivate = true;
        enemiesKilled = 0;
        enemiesSpawned = 0;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (enemiesSpawned < (enemiesPerWave * currentWave + 1))
        {
            spawner.SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(1f - (0.09f * (currentWave + 1)));
        }
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesPerWave)
        {
            waveActivate = false;

            StartNextWave();
        }
    }
}
