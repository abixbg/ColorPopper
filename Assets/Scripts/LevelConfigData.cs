using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[System.Serializable]
public struct LevelConfigData
{
    [SerializeField] private int2 boardSize;
    public int2 BoardSize => boardSize;
}
