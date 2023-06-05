using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player target;
    Rigidbody2D body;
    Collider2D coll;
    SpriteRenderer spriteRenderer;

    public Animator anim;
    public float moveSpeed;
    public int maxHealth;
    public int health;
    public int expNum;

    Vector2 direction;
    bool onDead;
    WaitForFixedUpdate WFS;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        WFS = new WaitForFixedUpdate();
    }
    private void OnEnable()
    {
        target = Player.Instance;
        coll.enabled = true;
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
        if (onDead || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        body.MovePosition(body.position + direction * moveSpeed * Time.fixedDeltaTime);
        body.velocity = Vector2.zero;
    }

    public void Init(SEnemyData data)
    {
        anim.runtimeAnimatorController = data.controller;
        moveSpeed = data.speed;
        maxHealth = data.health;
        health = data.health;
        expNum = data.expNum;
    }


    public void Hurt(int damage, Vector2 dir)
    {
        health -= damage;
        anim.SetTrigger("Hit");
        StartCoroutine(KnockBack(dir));

        if (health <= 0)
        {
            onDead = true;
            coll.enabled = false;
            anim.SetBool("Dead", true);

            GameManager.Instance.SpawnExp(expNum, transform.position);
            Invoke("Dead", 2f);
        }
    }
    IEnumerator KnockBack(Vector2 dir)
    {
        yield return WFS;

        if (!onDead)
            body.AddForce(dir.normalized * 3f, ForceMode2D.Impulse);
    }
    private void Dead()
    {
        body.velocity = Vector2.zero;
        PoolManager.Instance.Push(gameObject);
    }
}
