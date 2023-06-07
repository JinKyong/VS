using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovle : Weapon
{
    [SerializeField] BulletWeaponDataSO data;
    float count;

    public override void Init()
    {
        level = 0;

        damage = data.damage[level];
        coolTime = data.coolTime[level];

        count = coolTime;
    }
    public override void Levelup()
    {
        if (data.damage.Length == level + 1) return;

        level++;
        damage = data.damage[level];
        coolTime = data.coolTime[level];
    }

    private void Update()
    {
        if (!active) return;

        count -= Time.deltaTime;
        if(count <= 0)
        {
            //น฿ป็
            Bullet blt = PoolManager.Instance.Pop(data.bullet).GetComponent<Bullet>();
            blt.transform.position = transform.position;

            Transform target = Player.Instance.recognition.GetNearTarget();
            if (target is null)
                blt.Init(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
                    5f, damage);
            else
                blt.Init(target.position - transform.position,
                    5f, damage);
            count = coolTime;
        }
    }
}
