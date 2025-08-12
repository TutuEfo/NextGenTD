using System;
using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;

public class SlowRunTime
{
    public float percent;
    public float timeLeft;
}

public class StatusController : MonoBehaviour
{
    EnemyMovement move;
    readonly List<SlowRunTime> slows = new();

    private void Awake()
    {
        move = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = slows.Count - 1; i >= 0; i--)
        {
            slows[i].timeLeft -= dt;

            if (slows[i].timeLeft <= 0)
            {
                slows.RemoveAt(i);
            }
        }

        float minMultiplier = 1f;

        foreach (var s in slows)
        {
            minMultiplier = Mathf.Min(minMultiplier, 1f - s.percent);
        }

        move.SetSpeedMultiplier(minMultiplier);
    }

    [ContextMenu("Test Slow")]
    void TestSlow()
    {
        ApplySlow(0.4f, 2f);
    }

    public void ApplySlow(float percent, float duration)
    {
        slows.Add(new SlowRunTime{percent = Mathf.Clamp01(percent), timeLeft = duration});
    }
}
