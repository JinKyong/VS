using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player target;
    Rigidbody2D body;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    float moveSpeed;
    Vector2 direction;
    bool onDead;

    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance;
        body = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        onDead = false;
    }

    private void LateUpdate()
    {
        if (onDead) return;

        direction = (target.transform.position - transform.position).normalized;
        if (direction.x != 0)
            spriteRenderer.flipX = direction.x < 0 ? true : false;
    }

    private void FixedUpdate()
    {
        if (onDead) return;

        body.MovePosition(body.position + direction * moveSpeed * Time.fixedDeltaTime);
        body.velocity = Vector2.zero;
    }
}
