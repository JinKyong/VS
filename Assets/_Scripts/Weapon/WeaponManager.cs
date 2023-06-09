using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] Transform weaponTR;
    List<Weapon> weapons;

    //List<Select> selects;

    private void Start()
    {
        weapons = new List<Weapon>();
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
            weapon.Init(data);
            weapons.Add(weapon);
        }
        else
        {
            weapon.LevelUP(data, level);
        }
    }
}
