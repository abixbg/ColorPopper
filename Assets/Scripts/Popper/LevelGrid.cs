using AGK.GameGrids;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGrid : GameGrid2D<SlotData>
{
    public LevelGrid(int2 size, List<SlotData> nodes) : base(size, nodes)
    {
        foreach (var cell in nodes)
        {
            cell.Init(false, true);
        }
    }
}
