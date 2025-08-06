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
        
        Vector3 direction = Vector3.right;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetDirection((direction));
        fireTimer = 0f;
    }
}
