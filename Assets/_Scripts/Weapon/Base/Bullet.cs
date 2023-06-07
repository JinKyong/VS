using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;
    float speed;
    int damage;

    public void Init(Vector2 direction, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;

        if (collision.CompareTag("Enemy"))
        {
            PoolManager.Instance.Push(gameObject);

            collision.GetComponent<Enemy>().Hurt(damage,
                collision.transform.position - transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;

        if (collision.CompareTag("Area"))
        {
            PoolManager.Instance.Push(gameObject);
        }
    }
}