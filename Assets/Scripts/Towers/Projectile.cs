using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Damage & Effects")]
    public int damage = 1;
    public bool applySlow = false;
    public float slowPercent = 0.4f;
    public float slowDuration = 2f;
    
    public float speed = 5f;
    public float lifetime = 3f;
    public float attackRange = 3f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target == null)
        {
            target = FindClosestEnemy();

            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (applySlow)
            {
                var status = other.GetComponent<StatusController>();
                
                if (status)
                {
                    status.ApplySlow(slowPercent, slowDuration);
                }
            }

            Destroy(gameObject);
        }
    }

    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closest = null;

        float shortestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= shortestDistance)
            {
                shortestDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
