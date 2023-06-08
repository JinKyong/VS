using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] Transform weaponTR;
    List<Weapon> weapons;

    public float defaultAttackSpeed;

    //List<Select> selects;

    private void Start()
    {
        weapons = new List<Weapon>();
        defaultAttackSpeed = 1f;
    }

    private Weapon searchInven(WeaponData data)
    {
        foreach (var w in weapons)
        {
            if (w.weaponName.Equals(data.weaponName))
            {
                return w;
            }
        }

        return null;
    }
    public void AddWeapon(WeaponData data, int level)
    {
        Weapon weapon = searchInven(data);

        if (weapon is null)
        {
            weapon = Instantiate(data.weapon, weaponTR).GetComponent<Weapon>();
            weapon.weaponName = data.weaponName;
            weapons.Add(weapon);
        }

        weapon.damage += data.damage[level];
        weapon.count += data.counts[level];
    }
}
