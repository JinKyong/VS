using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] Transform selectTR;
    int selectChildCount;
    bool bFirst;

    [SerializeField] Transform weaponTR;
    [SerializeField] AudioSource selectSound;
    List<Weapon> weapons;
    int selectWeaponCount;
    int availableWeaponCount;

    private void Start()
    {
        selectChildCount = selectTR.childCount;
        bFirst = true;

        weapons = new List<Weapon>();
        selectWeaponCount = 3;

        for (int i = 0; i < selectChildCount; i++)
            selectTR.GetChild(i).gameObject.SetActive(false);

        selectChildCount -= 1;
        availableWeaponCount = selectChildCount;
    }

    public void EnableSelectWeapon()
    {
        Time.timeScale = 0f;

        if (availableWeaponCount <= 0)
        {
            selectTR.GetChild(selectTR.childCount - 1).gameObject.SetActive(true);
            return;
        }

        if (bFirst)
        {
            for (int i = 0; i < selectWeaponCount; i++)
                selectTR.GetChild(i).gameObject.SetActive(true);

            bFirst = false;
        }
        else
        {
            int count = 0;
            int infinity = 0;
            while (count < selectWeaponCount)
            {
                int rand = Random.Range(0, selectChildCount);

                Select select = selectTR.GetChild(rand).GetComponent<Select>();
                if (!select.IsMaxLevel && !select.gameObject.activeSelf)
                {
                    select.gameObject.SetActive(true);
                    count++;
                }

                infinity++;
                if(infinity > 1000)
                {
                    Debug.Log("무한루프");
                    break;
                }
            }
        }
    }
    public void DisableSelectWeapon()
    {
        Time.timeScale = 1f;
        selectSound.Play();
        foreach (Transform tr in selectTR)
            tr.gameObject.SetActive(false);
    }
    public void MaxLevelWeapon()
    {
        availableWeaponCount--;
        if (availableWeaponCount < selectWeaponCount)
            selectWeaponCount = availableWeaponCount;
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

        DisableSelectWeapon();
    }
}