using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognition : MonoBehaviour
{
    public float range;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    //public Transform nearTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0, targetLayer);
    }

    public Transform GetNearTarget()
    {
        if (targets.Length <= 0) return null;

        Array.Sort(targets, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(
            Vector2.Distance(transform.position, y.transform.position)));

        return targets[0].transform;
    }
}
