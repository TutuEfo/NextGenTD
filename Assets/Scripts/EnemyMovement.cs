using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] path;
    public float moveSpeed = 2f;

    private int currentIndex = 0;

    private void Update()
    {
        if (path == null || path.Length == 0)
        {
            return;
        }

        Transform target = path[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            currentIndex++;

            if (currentIndex >= path.Length)
            {
                Destroy(gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<EnemyHealth>().TakeDamage(3);
        }
    }

    public void SetPath(Transform[] waypoints)
    {
        path = waypoints;
    }
}
