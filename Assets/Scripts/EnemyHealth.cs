using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    private SpriteRenderer spriterenderer;
    public Transform healthBarParent;
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthBarParent != null && healthBarPrefab != null)
        {
            spriterenderer = GetComponent<SpriteRenderer>();

            GameObject healthObj = Instantiate(healthBarPrefab, healthBarParent.position, Quaternion.identity, healthBarParent);
            healthBar = healthObj.GetComponent<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP left: {currentHealth}");

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

        WaveManager waveManager = WaveManager.FindFirstObjectByType<WaveManager>();

        if (waveManager != null)
        {
            waveManager.OnEnemyKilled();
        }

        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator FlasRed()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.black;
    }
}
