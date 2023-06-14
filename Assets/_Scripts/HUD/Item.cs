using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Transform levelTR;

    public void Init(WeaponData data)
    {
        name = data.weaponName;
        icon.sprite = data.weaponIcon;

        for (int i = 0; i < data.damage.Length - 1; i++)
            levelTR.GetChild(i).gameObject.SetActive(true);
    }

    public void LevelUp(int level)
    {
        levelTR.GetChild(level - 1).GetChild(0).gameObject.SetActive(true);
    }
}
