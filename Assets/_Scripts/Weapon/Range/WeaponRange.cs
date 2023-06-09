using System.Collections;
using UnityEngine;

public class WeaponRange : Weapon
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float attackSpeed;

    float coolTime = 0;

    public override void LevelUP(WeaponData data, int level)
    {
        damage += data.damage[level];
        count += data.counts[level];

        coolTime = 0;
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        if(coolTime <= 0)
        {
            //น฿ป็
            StartCoroutine(BulletFire());
            coolTime = Player.Instance.Stat.AttackSpeed(attackSpeed);
        }
    }
    IEnumerator BulletFire()
    {
        for (int i = 0; i < count; i++)
        {
            fire();
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void fire()
    {
        Bullet blt = PoolManager.Instance.Pop(bullet).GetComponent<Bullet>();
        blt.transform.position = transform.position;

        Transform target = Player.Instance.recognition.GetNearTarget();
        if (target is null)
            blt.Init(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
                bulletSpeed, Player.Instance.Stat.Damage(damage));
        else
            blt.Init(target.position - transform.position,
                bulletSpeed, Player.Instance.Stat.Damage(damage));
    }
}
