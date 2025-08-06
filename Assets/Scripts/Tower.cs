using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float fireRate = 1f;
    private float fireTimer = 0f;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            // Temp shooter for tower.
            FireProjectile();

            fireTimer = 0f;
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            return;
        }
        
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Projectile projectile = proj.GetComponent<Projectile>();

        if (projectile == null)
        {
            return;
        }

        Transform target = FindClosestEnemy();

        if (target != null)
        {
            projectile.SetTarget(target);
        }
        else
        {
            Destroy(proj);
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
