using System;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelData",
    menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    [Serializable]
    public struct SRange
    {
        public int min;
        public int max;
    }
    [Serializable]
    public struct SRangeF
    {
        public float min;
        public float max;
    }

    public SRange[] enemyRange;
    public SRangeF[] spawnTerm;
    public int[] levelRange;
}
