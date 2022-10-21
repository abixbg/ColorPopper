using AGK.GameGrids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratorContentColor
{
    private readonly ISlotKeyPool keyPool;

    public GeneratorContentColor(ISlotKeyPool keyPool)
    {
        this.keyPool = keyPool;
    }

    public List<SlotContent> AddContent(GameGrid2D<SlotData> grid)
    {
        List<SlotContent> allKeys = new List<SlotContent>();

        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var slot = grid.Nodes[i];
            //Color color = ((ColorSlotKey)keyPool.GetRandom()).Color;
            slot.Content = keyPool.GetRandom();

            if (!allKeys.Any(c=> c.IsMatch(slot.Content)))
                allKeys.Add(slot.Content);
        }

        return allKeys;
    }
}
