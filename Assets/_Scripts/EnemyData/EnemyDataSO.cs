using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SEnemyData
{
    public RuntimeAnimatorController controller;
    public float speed;
    public int health;
}

[CreateAssetMenu(fileName = "EnemyData",
    menuName = "ScriptableObjects/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public List<SEnemyData> dataList;
}
