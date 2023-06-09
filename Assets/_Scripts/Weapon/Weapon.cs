
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [HideInInspector] public string weaponName;
    [HideInInspector] public int damage = 0;
    [HideInInspector] public float count = 0;

    public virtual void Init(WeaponData data)
    {
        weaponName = data.weaponName;
        damage = data.damage[0];
        count = data.counts[0];
    }
    public abstract void LevelUP(WeaponData data, int level);
}