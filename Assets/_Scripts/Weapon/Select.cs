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
    public bool IsMaxLevel { get { return level == data.damage.Length; } }

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
        level++;

        if (IsMaxLevel) WeaponManager.Instance.MaxLevelWeapon();
        else updateInfo();
    }
    public void SelectButtonEtc()
    {
        if(data.weaponName == "포션")
        {
            Player.Instance.UpdateHP(30);
        }
        else if(data.weaponName == "코인")
        {
            Player.Instance.GetCoin(50);
        }

        WeaponManager.Instance.DisableSelectWeapon();
    }

    private void updateInfo()
    {
        switch (data.weaponType) 
        {
            case WeaponData.EWeaponType.Melee:
            case WeaponData.EWeaponType.Range:
                weaponLevel.text = "Lv." + level;
                weaponDesc.text = string.Format(data.weaponDesc,
                    data.damage[level], data.counts[level]);
                break;
            case WeaponData.EWeaponType.Gear:
                weaponLevel.text = "Lv." + level;
                weaponDesc.text = string.Format(data.weaponDesc,
                    data.counts[level] * 100f);
                break;
            case WeaponData.EWeaponType.Etc:
                weaponDesc.text = data.weaponDesc;
                break;
            default:
                break;
        }
    }
}
