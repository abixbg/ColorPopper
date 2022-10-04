using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BubblePoolColors : ISlotKeyPool<ColorSlotKey>
{
    private readonly List<ColorSlotKey> colorKeys;

    public int Remaining => colorKeys.Count;

    public BubblePoolColors(ColorPalette palette)
    {
        colorKeys = new List<ColorSlotKey>();

        foreach (var col in palette.Colors)
        {
            colorKeys.Add(new ColorSlotKey(col));
        }
    }

    public List<ColorSlotKey> Pool => colorKeys;

    public ColorSlotKey GetRandom()
    {
            int index = Random.Range(0, colorKeys.Count);
        return colorKeys[index];
    }

    public ColorSlotKey GetRandomNew(ColorSlotKey current)
    {
        var newKey = GetRandom();

        while (newKey.Color == current.Color && Remaining > 1)
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
        int found = colorKeys.RemoveAll(k=> k.Color == key.Color);
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
}
