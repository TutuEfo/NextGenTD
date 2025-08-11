using System;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int playerGold = 100;
    public int playerLives = 10;
    public int totalWaves = 10;
    public int currentWave = 1;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public WaveManager waveManager;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;

    private bool gameEnded = false;

    public void EnemyReachGoal()
    {
        if (gameEnded)
        {
            return;
        }

        playerLives--;
        UpdateLivesUI();

        Debug.Log($"Player lives left: {playerLives}");

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    public void WaveCompleted()
    {
        Debug.Log("Wave completed: " + currentWave);

        currentWave++;
        UpdateWaveUI();

        if (currentWave > totalWaves)
        {
            WinGame();
        }
    }

    private void GameOver()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
        UIButtons.PauseGameUI();
    }

    private void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        UIButtons.PauseGameUI();
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateGoldUI();
        UpdateLivesUI();
        UpdateWaveUI();
    }

    public bool SpendGold(int amount)
    {
        if (playerGold >= amount)
        {
            playerGold -= amount;
            UpdateGoldUI();

            return true;
        }

        return false;
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = $"Gold: {playerGold}";
        }
    }

    public void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {playerLives}";
        }
    }

    public void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave: {currentWave}";
        }
    }
}
