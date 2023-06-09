using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField] int value;
    float moveSpeed;
    Transform target;
    bool bChase;

    public void OnTarget()
    {
        target = Player.Instance.transform;
        bChase = true;
    }

    private void Start()
    {
        moveSpeed = 6f;
        target = null;
        bChase = false;
    }
    private void Update()
    {
        if (bChase)
            transform.Translate((target.position - transform.position) * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.GetExp(value);
            PoolManager.Instance.Push(gameObject);
        }
    }
}