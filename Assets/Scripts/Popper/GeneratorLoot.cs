using AGK.GameGrids;
using AGK.GameGrids.CellGroups;
using System.Collections;
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
        var islandFinder = new Islandfinder<GameGrid2D<SlotData>, SlotData>(grid);
        islandFinder.RecalculateIslands();
        var islands = islandFinder.GetIslands(3);

        Debug.LogError($"Islands: {islands.Count}");

        foreach (var island in islands)
        {
            var slot = grid.GetNodeAt(island.Cells[0].Position);

            slot.Loot = new LootStar();

            ////connected slots
            //List<SlotVisual> connected = new List<SlotVisual>();

            //for (int i = 1; i < island.Cells.Count; i++)
            //{
            //    connected.Add(grid.GetNodeAt(island.Cells[i].Position).SlotVisual);
            //}

            //AddIslandDestructLoot(slot, connected);
            //Debug.LogWarning($"[BoardVisual] {island.ToString()} --> {island.Cells[0].Position} ");
        }



        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var slot = grid.Nodes[i];

        }
    }

    //private void AddLoot()
    //{
    //    var islandFinder = new Islandfinder<GameGrid2D<SlotData>, SlotData>(_grid);
    //    islandFinder.RecalculateIslands();
    //    var islands = islandFinder.GetIslands(3);

    //    Debug.LogError($"Islands: {islands.Count}");

    //    foreach (var island in islands)
    //    {
    //        var slot = _grid.GetNodeAt(island.Cells[0].Position);

    //        //connected slots
    //        List<SlotVisual> connected = new List<SlotVisual>();

    //        for (int i = 1; i < island.Cells.Count; i++)
    //        {
    //            connected.Add(_grid.GetNodeAt(island.Cells[i].Position).SlotVisual);
    //        }

    //        AddIslandDestructLoot(slot, connected);
    //        Debug.LogWarning($"[BoardVisual] {island.ToString()} --> {island.Cells[0].Position} ");
    //    }
    //}

    //private void AddIslandDestructLoot(SlotData slot, List<SlotVisual> connected)
    //{
    //    bool confirmed = UnityEngine.Random.value >= 0.6f;

    //    if (true)
    //    {
    //        //instantiating dots in grid
    //        slot.SlotVisual.Loot = Object.Instantiate(lootPrefab, slot.SlotVisual.transform.position, Quaternion.identity) as Loot;
    //        slot.SlotVisual.Loot.Construct(GameManager.current.Events, GameManager.current.UiManager, connected);

    //        //make dot gameobjects parent of slot
    //        slot.SlotVisual.Loot.transform.parent = slot.SlotVisual.transform;
    //    }
    //}
}
