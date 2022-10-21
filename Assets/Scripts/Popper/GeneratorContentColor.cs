using AGK.GameGrids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratorContentColor
{
    private readonly ISlotKeyPool<ColorSlotKey> keyPool;

    public GeneratorContentColor(ISlotKeyPool<ColorSlotKey> keyPool)
    {
        this.keyPool = keyPool;
    }

    public List<SlotContent> AddContent(GameGrid2D<SlotData> grid)
    {
        List<SlotContent> allKeys = new List<SlotContent>();

        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var slot = grid.Nodes[i];
            Color color = keyPool.GetRandom().Color;
            slot.Content = new ColorSlotKey(color);

            if (!allKeys.Any(c=> c.IsMatch(slot.Content)))
                allKeys.Add(slot.Content);
        }

        return allKeys;
    }
}
