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

    public SRange[] levels;
    public int[] levelRange;
}
