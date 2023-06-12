using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeaponData",
    menuName = "ScriptableObjects/WeaponData/BaseWeaponData")]
public class WeaponData : ScriptableObject
{
    public enum EWeaponType { Melee, Range, Gear, Etc}

    [Header("Main Info")]
    public EWeaponType weaponType;
    public string weaponName;
    [TextArea]
    public string weaponDesc;
    public Sprite weaponIcon;

    [Space]
    [Header("Level Data")]
    public int[] damage;
    public float[] counts;

    [Space]
    [Header("Prefab")]
    public GameObject weapon;
}
