using AGK.GameGrids;
using AGK.GameGrids.CellGroups;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLoot
{
    private readonly GameGrid2D<SlotData> grid;

    public GeneratorLoot(GameGrid2D<SlotData> grid)
    {
        this.grid = grid;
    }

    public void GenerateLoot()
    {
        //for (int i = 0; i < grid.Nodes.Count; i++)
        //{
        //    var slot = grid.Nodes[i];
        //}

        GenerateBombIslands();
    }

    private void GenerateBombIslands()
    {
        var islandFinder = new Islandfinder<GameGrid2D<SlotData>, SlotData>(grid);
        islandFinder.RecalculateIslands();
        var islands = islandFinder.GetIslands(3);

        foreach (var island in islands)
        {
            var slot = grid.GetNodeAt(island.Cells[0].Position);

            //connected slots
            List<SlotData> connected = new List<SlotData>();

            for (int i = 1; i < island.Cells.Count; i++)
            {
                connected.Add(grid.GetNodeAt(island.Cells[i].Position));
            }

            slot.Loot = new LootStar(connected);

            Debug.LogWarning($"[GeneratorLoot] {island} --> {island.Cells[0].Position} ");
        }
    }
}
