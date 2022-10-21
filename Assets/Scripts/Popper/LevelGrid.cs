using AGK.GameGrids;
using System.Collections.Generic;
using Unity.Mathematics;

public class LevelGrid : GameGrid2D<SlotData>
{
    public List<SlotContent> AllKeys { get; private set; }

    public LevelGrid(int2 size, List<SlotData> nodes) : base(size, nodes)
    {
        ResetCellsState();
        ResetCellsContent();
    }

    public void ResetCellsState()
    {
        foreach (var cell in Nodes)
        {
            cell.IsActive = true;
            cell.IsBroken = false;
        }
    }
    public void ResetCellsContent()
    {
        foreach (var cell in Nodes)
        {
            cell.Content = null;
            cell.Loot = null;
        }
    }

    public bool HaveKeyHoleOnBoard(SlotContent key)
    {
        foreach (var slot in Nodes)
        {
            if (slot.Content.IsMatch(key) && slot.IsActive)
            {
                return true;
            }
        }

        return false;
    }

    public void AddContent(GeneratorContentColor generator)
    {
        AllKeys = generator.AddContent(this);
    }

    public void AddLoot(GeneratorLoot generator)
    {
        generator.AddLoot(this);
    }
}