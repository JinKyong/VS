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
    public int damage;
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

        onDead = false;
        coll.enabled = true;
        body.simulated = true;
        spriteRenderer.sortingOrder = 0;
        //anim.SetBool("Dead", false);
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
        damage = data.damage;
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
            body.simulated = false;
            spriteRenderer.sortingOrder = -1;
            anim.SetBool("Dead", true);

            HUD.Instance.KillEnemy();
            GameManager.Instance.SpawnExp(expNum, transform.position);
        }
    }
    IEnumerator KnockBack(Vector2 dir)
    {
        yield return WFS;
        
        body.AddForce(dir.normalized * 3f, ForceMode2D.Impulse);
    }
    public void Dead()
    {
        PoolManager.Instance.Push(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().Hurt(damage);
        }
    }
}
