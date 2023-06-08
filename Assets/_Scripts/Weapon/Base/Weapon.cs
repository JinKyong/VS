using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [HideInInspector] public string weaponName;
    [HideInInspector] public int damage;
    [HideInInspector] public float count;
}