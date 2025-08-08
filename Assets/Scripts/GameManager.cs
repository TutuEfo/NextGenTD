using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int playerGold = 100;
    public TextMeshProUGUI goldText;

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
}
