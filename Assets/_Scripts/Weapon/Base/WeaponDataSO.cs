using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeaponData",
    menuName = "ScriptableObjects/BaseWeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public int[] damage;
    public float[] coolTime;
}
