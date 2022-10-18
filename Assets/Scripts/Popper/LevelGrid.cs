using AGK.GameGrids;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGrid : GameGrid2D<SlotData>
{
    public LevelGrid(int2 size, List<SlotData> nodes) : base(size, nodes)
    {
        ResetCells();
    }

    public void ResetCells()
    {
        foreach (var cell in Nodes)
        {
            cell.Init(false, true);
        }
    }
}