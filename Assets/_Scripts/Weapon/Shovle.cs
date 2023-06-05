using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovle : Weapon
{
    [SerializeField]
    GameObject bullet;
    Collider2D target;
    List<Collider2D> targetList;

    float count;

    public override void Init()
    {
        targetList = Player.Instance.recognition.targetList;

        level = 1;
        maxLevel = 6;

        damage = 15;
        coolTime = 1f;

        count = coolTime;
    }
    public override void Levelup()
    {
        switch (level)
        {
            case 1:
                damage += 10;
                break;
            case 2:
                coolTime -= 0.25f;
                break;
            case 3:
                damage += 10;
                break;
            case 4:
                damage += 10;
                coolTime -= 0.25f;
                break;
            case 5:
                break;
            default:
                break;
        }

        level++;
    }

    private void Update()
    {
        if (!active) return;

        count -= Time.deltaTime;
        if(count <= 0)
        {
            //น฿ป็
            Bullet blt = PoolManager.Instance.Pop(bullet).GetComponent<Bullet>();
            blt.transform.position = transform.position;

            updateTarget();
            if (target is null)
                blt.Init(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
                    5f, damage);
            else
                blt.Init(target.transform.position - transform.position,
                    5f, damage);
            count = coolTime;
        }
    }
    private void updateTarget()
    {
        if (targetList.Count <= 0) target = null;
        else
        {
            targetList.Sort((x, y) => Vector2.Distance(transform.position, x.transform.position).
            CompareTo(Vector2.Distance(transform.position, y.transform.position)));

            target = targetList[0];
        }
    }
}
