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
    [SerializeField] Inventory inventory;
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

        selectChildCount -= 2;
        availableWeaponCount = selectChildCount;
    }

    public void EnableSelectWeapon()
    {
        Time.timeScale = 0f;

        if (availableWeaponCount <= 0)
        {
            selectTR.GetChild(selectTR.childCount - 2).gameObject.SetActive(true);
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
            while (count < selectWeaponCount)
            {
                int rand = Random.Range(0, selectChildCount);

                Select select = selectTR.GetChild(rand).GetComponent<Select>();
                if (!select.IsMaxLevel && !select.gameObject.activeSelf)
                {
                    select.gameObject.SetActive(true);
                    count++;
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
        //1. 인벤에 이미 있는 무기인지 확인
        Weapon weapon = searchInven(data);

        //1-1. 없는 무기면 새로 생성해서 추가
        if (weapon is null)
        {
            weapon = Instantiate(data.weapon, weaponTR).GetComponent<Weapon>();
            weapon.Init(data);

            weapons.Add(weapon);
            inventory.AddItem(data);
        }
        //1-2. 있는 무기면 해당 무기 레벨업
        else
        {
            weapon.LevelUP(data, level);
            inventory.LevelUp(data, level);
        }

        DisableSelectWeapon();
    }
}