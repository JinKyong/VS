using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player target;
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;

    public Animator anim;
    public float moveSpeed;
    public int maxHealth;
    public int health;

    Vector2 direction;
    bool onDead;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        target = Player.Instance;
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

    public void Init(SEnemyData data)
    {
        anim.runtimeAnimatorController = data.controller;
        moveSpeed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    public void Hurt(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            onDead = true;
            anim.SetBool("Dead", true);
            Invoke("Dead", 2f);
        }
    }
    private void Dead()
    {
        PoolManager.Instance.Pop(gameObject);
    }
}
