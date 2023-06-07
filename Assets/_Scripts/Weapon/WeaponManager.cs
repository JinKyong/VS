using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weaponPrefabs;

    public void AddWeapon(int num)
    {
        Weapon weapon = Instantiate(weaponPrefabs[num]).GetComponent<Weapon>();
        weapon.AddWeapon();
    }
}
