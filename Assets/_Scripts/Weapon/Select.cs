using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    [SerializeField] WeaponData data;

    [SerializeField] Image weaponIcon;
    [SerializeField] Text weaponName;
    [SerializeField] Text weaponLevel;
    [SerializeField] Text weaponDesc;

    int level;

    private void Start()
    {
        weaponIcon.sprite = data.weaponIcon;
        weaponName.text = data.weaponName;

        level = 0;
        updateInfo();
    }

    public void SelectButton()
    {
        WeaponManager.Instance.AddWeapon(data, level);

        if (level < data.damage.Length - 1)
        {
            level++;
            updateInfo();
        }
    }

    private void updateInfo()
    {
        weaponLevel.text = "Lv." + level;
        switch (data.weaponType) 
        {
            case WeaponData.EWeaponType.Melee:
            case WeaponData.EWeaponType.Range:
                weaponDesc.text = string.Format(data.weaponDesc,
                    data.damage[level], data.counts[level]);
                break;
            case WeaponData.EWeaponType.Gear:
                weaponDesc.text = string.Format(data.weaponDesc,
                    data.counts[level] * 100f);
                break;
            case WeaponData.EWeaponType.Potion:
                break;
            default:
                break;
        }
    }
}
