using System.Collections.Generic;
using UnityEngine;

public class BubblePoolColors
{
    //private readonly ColorPalette _palette;
    private readonly List<ColorSlotKey> colorKeys;

    public BubblePoolColors(ColorPalette palette)
    {
        colorKeys = new List<ColorSlotKey>();

        foreach (var col in palette.Colors)
        {
            colorKeys.Add(new ColorSlotKey(col));
        }
    }

    public ColorSlotKey GetRandomColor()
    {
        int index = Random.Range(0, colorKeys.Count - 1);

        return colorKeys[index];
    }
}
