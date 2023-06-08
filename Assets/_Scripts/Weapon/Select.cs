using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    [SerializeField] WeaponData data;

    [SerializeField] Image weaponIcon;
    [SerializeField] Text weaponLevel;
    [SerializeField] Text weaponName;
    [SerializeField] Text weaponDesc;

    int level;

    private void Start()
    {
        level = 0;

        weaponIcon.sprite = data.weaponIcon;
        weaponLevel.text = "Lv." + level;
        weaponName.text = data.weaponName;
        weaponDesc.text = string.Format(data.weaponDesc,
                     data.damage[level], data.counts[level]);
    }

    public void SelectButton()
    {
        WeaponManager.Instance.AddWeapon(data, level);

        level++;
        weaponLevel.text = "Lv." + level;
        weaponDesc.text = string.Format(data.weaponDesc,
                     data.damage[level], data.counts[level]);
    }
}
