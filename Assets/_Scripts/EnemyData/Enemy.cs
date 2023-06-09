using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player target;
    Rigidbody2D body;
    Collider2D coll;
    SpriteRenderer spriteRenderer;

    [SerializeField] EnemyDataSO dataSO;
    List<SEnemyData> dataList;
    int number;

    [SerializeField] Animator anim;
    public int health;

    [SerializeField] GameEvent enemyDead;
    Vector2 direction;
    bool onDead;
    WaitForFixedUpdate WFS;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        dataList = dataSO.dataList;
        WFS = new WaitForFixedUpdate();
    }
    private void OnEnable()
    {
        onDead = false;
        coll.enabled = true;
        body.simulated = true;
        spriteRenderer.sortingOrder = 0;
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

        body.MovePosition(body.position + direction * dataList[number].speed * Time.fixedDeltaTime);
        body.velocity = Vector2.zero;
    }

    public void Init(int num)
    {
        number = num;

        target = Player.Instance;
        anim.runtimeAnimatorController = dataList[number].controller;
        health = dataList[number].health;
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

            enemyDead.Raise();
            GameManager.Instance.SpawnExp(dataList[number].expNum, transform.position);
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
            collision.transform.GetComponent<Player>().Hurt(dataList[number].damage);
        }
    }
}
