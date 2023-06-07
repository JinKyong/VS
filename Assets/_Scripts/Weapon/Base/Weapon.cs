using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool active = false;

    protected int level;
    public int Level { get { return level; } }

    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float coolTime;

    public abstract void Init();
    public abstract void Levelup();

    public void AddWeapon()
    {
        if (Player.Instance.AddWeapon(this))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            active = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddWeapon();
        }
    }
}