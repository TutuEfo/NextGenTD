using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] path;

    public float baseSpeed = 2f;
    private float speedMultiplier = 1f;
    public void SetSpeedMultiplier(float m) { speedMultiplier = Mathf.Max(0.05f, m); }

    public float moveSpeed;
    public GameManager manager;

    private int currentIndex = 0;

    private void Awake()
    {
        SetSpeedMultiplier(1f);
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        moveSpeed = baseSpeed * speedMultiplier;

        if (path == null || path.Length == 0)
        {
            return;
        }

        Transform target = path[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            currentIndex++;

            if (currentIndex >= path.Length)
            {
                var wm = FindFirstObjectByType<WaveManager>();
                wm?.OnEnemyRemoved();

                manager.EnemyReachGoal();
                Destroy(gameObject);
            }
        }
    }

    public void SetPath(Transform[] waypoints)
    {
        path = waypoints;
    }
}
