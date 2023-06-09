using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon
{

    [SerializeField] GameObject rotatePrefab;
    [SerializeField] float hypotenuse;  //ºøº¯
    List<RotateObject> rotates;
    Transform rotateTR;
    Coroutine rotateRoutine;

    [SerializeField] float term;
    float rotSpeed;
    float coolTime;

    public override void Init(WeaponData data)
    {
        base.Init(data);

        rotates = new List<RotateObject>();
        rotateTR = transform.GetChild(0);

        rotSpeed = 60f;
        coolTime = 0;

        addRotate();
    }
    public override void LevelUP(WeaponData data, int level)
    {
        damage += data.damage[level];
        count += data.counts[level];

        coolTime = 0;
        addRotate();
    }

    private void addRotate()
    {
        if (rotates.Count >= count) return;

        if (rotateRoutine is not null)
        {
            StopCoroutine(rotateRoutine);
            resetRotateTR();
        }

        while (rotates.Count < count)
        {
            RotateObject r = Instantiate(rotatePrefab, rotateTR).GetComponent<RotateObject>();
            r.Init(damage);

            rotates.Add(r);
        }
        allignObject();
    }
    private void allignObject()
    {
        for (int i = 0; i < count; i++)
        {
            float angle = i * (360 / count);
            float x = Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * hypotenuse;
            float y = Mathf.Sin((angle + 90) * Mathf.Deg2Rad) * hypotenuse;

            rotates[i].transform.localPosition = new Vector2(x, y);
            rotates[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    IEnumerator StartRotate()
    {
        rotateTR.gameObject.SetActive(true);
        float count = term / 2f;
        Vector3 rotateVector = new Vector3(0, 0, -1);

        while (count > 0)
        {
            rotateTR.Rotate(rotateVector * rotSpeed * Time.deltaTime);
            yield return null;
            rotSpeed += 150f * Time.deltaTime;
            count -= Time.deltaTime;
        }

        resetRotateTR();
        rotateRoutine = null;
    }
    private void resetRotateTR()
    {
        rotateTR.rotation = Quaternion.Euler(Vector3.zero);
        rotSpeed = 60f;
        rotateTR.gameObject.SetActive(false);
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        if(coolTime <= 0)
        {
            rotateRoutine = StartCoroutine(StartRotate());
            coolTime = Player.Instance.Stat.AttackSpeed(term);
        }
    }
}
