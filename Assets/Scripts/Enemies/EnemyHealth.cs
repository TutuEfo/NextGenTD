using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public Color defaultColor;

    private SpriteRenderer spriterenderer;
    public Transform healthBarParent;
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    WaveManager waveManager;

    private void Awake()
    {
        currentHealth = maxHealth;
        waveManager = FindFirstObjectByType<WaveManager>();

        if (healthBarParent != null && healthBarPrefab != null)
        {
            spriterenderer = GetComponent<SpriteRenderer>();

            defaultColor = spriterenderer.color;

            GameObject healthObj = Instantiate(healthBarPrefab, healthBarParent.position, Quaternion.identity, healthBarParent);
            healthBar = healthObj.GetComponent<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (spriterenderer != null)
        {
            StartCoroutine(FlasRed());
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.AddGold(10);
        var wm = FindFirstObjectByType<WaveManager>();
        wm?.OnEnemyRemoved();
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator FlasRed()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = defaultColor;
    }
}
