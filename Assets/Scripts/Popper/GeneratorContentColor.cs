using AGK.GameGrids;
using UnityEngine;

public class GeneratorContentColor
{
    private readonly GameGrid2D<SlotData> grid;
    private readonly ISlotKeyPool<ColorSlotKey> keyPool;

    public GeneratorContentColor(GameGrid2D<SlotData> grid, ISlotKeyPool<ColorSlotKey> keyPool)
    {
        this.grid = grid;
        this.keyPool = keyPool;
    }

    public void AddContent()
    {
        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var slot = grid.Nodes[i];
            Color color = keyPool.GetRandom().Color;
            slot.Content = new ColorSlotKey(color);
        }
    }
}
