using Popper.Events;
using System.Collections.Generic;
using UnityEngine;

public class BubblePoolColors : ISlotKeyPool
{
    private readonly List<ColorSlotKey> colorKeys;

    public int Remaining => colorKeys.Count;

    public BubblePoolColors(ColorPalette palette)
    {
        colorKeys = BuildFromPallete(palette);
    }

    public BubblePoolColors(List<SlotContent> keys)
    {
        colorKeys = new List<ColorSlotKey>();

        foreach (var key in keys)
        {
            if (key is ColorSlotKey)
            {
                colorKeys.Add((ColorSlotKey)key);
            }            
        }
    }

    public List<ColorSlotKey> Pool => colorKeys;

    public SlotContent GetRandom()
    {
        int index = Random.Range(0, colorKeys.Count);
        return colorKeys[index];
    }

    public SlotContent GetRandomNew(SlotContent current)
    {
        var newKey = GetRandom();

        while (((ColorSlotKey)newKey).Color == ((ColorSlotKey)current).Color && Remaining > 1)
            newKey = GetRandom();

        return newKey;
    }

    public void Replace(List<ColorSlotKey> keys)
    {
        colorKeys.Clear();
        foreach (var key in keys)
        {
            colorKeys.Add(key);
        }
    }

    public bool Remove(ColorSlotKey key)
    {
        int found = colorKeys.RemoveAll(k => k.Color == key.Color);
        return found > 0;
    }

    public bool Contains(ColorSlotKey key)
    {
        foreach (var k in colorKeys)
        {
            if (k.Color == key.Color)
                return true;
        }

        return false;
    }

    private List<ColorSlotKey> BuildFromPallete(ColorPalette palette)
    {
        List<ColorSlotKey> colorKeys = new List<ColorSlotKey>();

        foreach (var col in palette.Colors)
        {
            colorKeys.Add(new ColorSlotKey(col));
        }

        return colorKeys;
    }
}
