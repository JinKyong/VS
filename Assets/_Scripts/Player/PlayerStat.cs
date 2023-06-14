using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStat",
    menuName ="ScriptableObjects/Player/Stat")]
public class PlayerStat : ScriptableObject
{
    [Header("Initial Value")]
    [SerializeField] int iLevel;
    [SerializeField] int iExp;
    [SerializeField] int iHealth;
    [SerializeField] int iCoin;

    [Space]
    [Header("Initial Ratio")]
    [SerializeField] float iDamageRatio; 
    [SerializeField] float iSpeedRatio;
    [SerializeField] float iAttackSpeedRatio;
    [SerializeField] float iMagnetRatio;

    public void Init()
    {
        level = iLevel;
        exp = iExp;
        coin = iCoin;
        health = maxHealth = iHealth;

        damageRatio = iDamageRatio;
        speedRatio = iSpeedRatio;
        attackSpeedRatio = iAttackSpeedRatio;
        magnetRatio = iMagnetRatio;
    }

    [Header("Level")]
    //public
    public int level;
    public int exp;
    [SerializeField] int[] maxEXP;
    public int MaxEXP
    {
        get
        {
            if (level < maxEXP.Length) return maxEXP[level];
            else return maxEXP[maxEXP.Length - 1];
        }
    }
    public int coin;

    [Space]
    [Header("HP")]
    public int maxHealth;
    public int health;

    [Space]
    [Header("Stat")]
    public float damageRatio;
    public int Damage(int value) { return (int)(value * damageRatio); }

    [SerializeField] float speed;
    public float speedRatio;
    public float Speed { get { return speed * speedRatio; } }

    [SerializeField] float attackSpeed;
    public float attackSpeedRatio;
    public float AttackSpeed(float value) { return value * (attackSpeed / attackSpeedRatio); }

    [SerializeField] float magnetSize;
    public float magnetRatio;
    public float MagnetSize { get { return magnetSize * magnetRatio; } }
}