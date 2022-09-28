using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[System.Serializable]
public struct LevelConfigData
{
    [SerializeField] private int2 boardSize;
    [SerializeField] private int timeSec;
    [SerializeField] private ColorPalette palette;
    public int2 BoardSize => boardSize;
    public int TimeSec => timeSec;
    public ColorPalette Pallete => palette;
}
