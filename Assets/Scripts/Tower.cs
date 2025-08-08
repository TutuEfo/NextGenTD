using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    public GameObject projectilePrefab;
    public float attackRange = 3f;
    public float fireRate = 0.6f;
    public int towerDamage = 3;

    private float fireTimer = 0f;
    private Transform rangeIndicator;

    private static Tower selectedTower = null;

    private int upgradeLevel = 0;
    private int maxUpgradeLevel = 3;
    private int upgradeCost = 30;

    private void Start()
    {
        rangeIndicator = transform.Find("RangeIndicator/RangeCircle");

        if (rangeIndicator != null)
        {
            SpriteRenderer sr = rangeIndicator.GetComponent<SpriteRenderer>();

            if (sr != null && sr.sprite != null)
            {
                float spriteSize = sr.sprite.bounds.size.x;
                float desiredSize = attackRange * 2f;
                float scaleFactor = desiredSize / spriteSize;

                rangeIndicator.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
                rangeIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("RangeIndicator/RangeCircle not found in prefab!");
        }
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Transform target = FindClosestEnemyInRange();

            if (target != null)
            {
                FireProjectile(target);
                fireTimer = 0f;
            }
        }
    }

    public bool Upgrade()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Max upgrade level reached!");
            return false;
        }

        upgradeLevel++;

        if (!GameManager.Instance.SpendGold(upgradeCost * upgradeLevel))
        {
            Debug.Log("Not enough gold!");
            upgradeLevel--;
            return false;
        }

        attackRange += 0.5f;
        fireRate -= 0.05f;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();

        towerDamage = 5;

        return true;
    }

    private void FireProjectile(Transform target)
    {
        if (projectilePrefab == null || target == null)
            return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.damage = towerDamage;
            projectile.SetTarget(target);
        }
    }

    private Transform FindClosestEnemyInRange()
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

    private void OnMouseDown()
    {
        var selector = FindObjectOfType<TowerSelector>();
        if (selector == null)
            return;

        if (selectedTower != null && selectedTower != this)
            selectedTower.HideRange();

        if (selectedTower == this)
        {
            HideRange();
            selectedTower = null;
            selector.DeselectTower();
        }
        else
        {
            ShowRange();
            selectedTower = this;
            selector.SelectTower(this);
        }
    }

    private void ShowRange()
    {
        if (rangeIndicator != null)
            rangeIndicator.gameObject.SetActive(true);
    }

    private void HideRange()
    {
        if (rangeIndicator != null)
            rangeIndicator.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null || hit.collider.GetComponent<Tower>() == null)
            {
                if (selectedTower != null)
                {
                    selectedTower.HideRange();
                    selectedTower = null;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
