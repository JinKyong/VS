using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StatData",
    menuName = "ScriptableObjects/Player/StatData")]
public class StatData : ScriptableObject
{
    public enum EStatDataType
    {
        Health, HealthRegen, Magent, Damage, AttackSpeed, Speed,
    }

    public EStatDataType type;
    public Sprite icon;
    public string statName;
    public int maxLevel;
    public float value;
    public int cost;
}
