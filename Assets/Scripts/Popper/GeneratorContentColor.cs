using AGK.GameGrids;
using UnityEngine;

public class GeneratorContentColor
{
    private readonly ISlotKeyPool<ColorSlotKey> keyPool;

    public GeneratorContentColor(ISlotKeyPool<ColorSlotKey> keyPool)
    {
        this.keyPool = keyPool;
    }

    public void AddContent(GameGrid2D<SlotData> grid)
    {
        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var slot = grid.Nodes[i];
            Color color = keyPool.GetRandom().Color;
            slot.Content = new ColorSlotKey(color);
        }
    }
}
