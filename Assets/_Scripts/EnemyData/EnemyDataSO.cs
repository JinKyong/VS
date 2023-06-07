using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SEnemyData
{
    public RuntimeAnimatorController controller;
    public float speed;
    public int health;
    public int damage;
    public int expNum;
}

[CreateAssetMenu(fileName = "EnemyData",
    menuName = "ScriptableObjects/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public List<SEnemyData> dataList;
}
