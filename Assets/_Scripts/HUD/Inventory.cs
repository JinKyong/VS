using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform weaponTR;
    [SerializeField] Transform gearTR;
    [SerializeField] GameObject itemPrefab;

    public void AddItem(WeaponData data)
    {
        Item item = Instantiate(itemPrefab, transform).GetComponent<Item>();
        switch (data.weaponType)
        {
            case WeaponData.EWeaponType.Melee:
            case WeaponData.EWeaponType.Range:
                item.transform.SetParent(weaponTR);
                break;

            case WeaponData.EWeaponType.Gear:
                item.transform.SetParent(gearTR);
                break;

            default:
                break;
        }

        item.Init(data);
    }
    public void LevelUp(WeaponData data, int level)
    {
        Transform tr;
        switch (data.weaponType)
        {
            case WeaponData.EWeaponType.Melee:
            case WeaponData.EWeaponType.Range:
                tr = weaponTR;
                break;

            case WeaponData.EWeaponType.Gear:
                tr = gearTR;
                break;

            default:
                return;
        }

        foreach (Transform i in tr)
        {
            if(i.name == data.weaponName)
            {
                i.GetComponent<Item>().LevelUp(level);
                break;
            }
        }
    }
}
