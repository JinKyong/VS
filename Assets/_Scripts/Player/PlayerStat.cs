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
    [SerializeField] float iHealth;
    [SerializeField] float iHealthRegen;
    [SerializeField] int iCoin;

    [Space]
    [Header("Initial Ratio")]
    [SerializeField] float iMagnetRatio;
    [SerializeField] float iDamageRatio; 
    [SerializeField] float iAttackSpeedRatio;
    [SerializeField] float iSpeedRatio;

    [Space]
    [Header("Upgrade")]
    [SerializeField] public float UHealth;
    [SerializeField] public float UHealthRegen;
    [SerializeField] public float UMagnetRatio;
    [SerializeField] public float UDamageRatio;
    [SerializeField] public float UAttackSpeedRatio;
    [SerializeField] public float USpeedRatio;

    public void Init()
    {
        totalCoin = PlayerPrefs.HasKey("TotalCoin") ? PlayerPrefs.GetInt("TotalCoin") : 0;
    }
    public void GameStart()
    {
        level = iLevel;
        exp = iExp;
        coin = iCoin;
        health = maxHealth = iHealth + UHealth;
        healthRegen = iHealthRegen + UHealthRegen;

        magnetRatio = iMagnetRatio + UMagnetRatio;
        damageRatio = iDamageRatio + UDamageRatio;
        speedRatio = iSpeedRatio + USpeedRatio;
        attackSpeedRatio = iAttackSpeedRatio + UAttackSpeedRatio;
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
    public int totalCoin;

    [Space]
    [Header("HP")]
    public float maxHealth;
    public float health;
    public float healthRegen;

    [Space]
    [Header("Stat")]
    [SerializeField] float magnetSize;
    public float magnetRatio;
    public float MagnetSize { get { return magnetSize * magnetRatio; } }

    public float damageRatio;
    public int Damage(int value) { return (int)(value * damageRatio); }

    [SerializeField] float speed;
    public float speedRatio;
    public float Speed { get { return speed * speedRatio; } }

    [SerializeField] float attackSpeed;
    public float attackSpeedRatio;
    public float AttackSpeed(float value) { return value * (attackSpeed / attackSpeedRatio); }
}