
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    int damage;

    public void Init(int damage) 
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Hurt(damage,
                collision.transform.position - transform.position);
        }
    }
}
