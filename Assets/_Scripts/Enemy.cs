using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject target;
    Rigidbody2D body;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    float moveSpeed;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.player.gameObject;
        body = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        direction = (target.transform.position - transform.position).normalized;
        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0 ? true : false;
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + direction * moveSpeed * Time.deltaTime);
    }
}
