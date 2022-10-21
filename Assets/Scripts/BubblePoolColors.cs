using System.Collections.Generic;
using UnityEngine;

public class BubblePoolColors : ISlotKeyPool
{
    private readonly List<ColorSlotKey> _colorKeys;

    public BubblePoolColors(ColorPalette palette)
    {
        _colorKeys = new List<ColorSlotKey>();

        foreach (var col in palette.Colors)
        {
            _colorKeys.Add(new ColorSlotKey(col));
        }
    }

    public BubblePoolColors(List<SlotContent> keys)
    {
        _colorKeys = new List<ColorSlotKey>();

        foreach (var key in keys)
        {
            if (key is ColorSlotKey)
            {
                _colorKeys.Add((ColorSlotKey)key);
            }
        }
    }

    public int Remaining => _colorKeys.Count;
    public List<ColorSlotKey> Pool => _colorKeys;

    #region ISlotKeyPool
    SlotContent ISlotKeyPool.GetRandom()
    {
        int index = Random.Range(0, _colorKeys.Count);
        return _colorKeys[index];
    }

    SlotContent ISlotKeyPool.GetRandomNew(SlotContent current)
    {
        var newKey = GetRandomColorKey();

        while (newKey.IsMatch(current) && Remaining > 1)
            newKey = GetRandomColorKey();

        return newKey;
    }
    #endregion

    private ColorSlotKey GetRandomColorKey()
    {
        int index = Random.Range(0, _colorKeys.Count);
        return _colorKeys[index];
    }

    public void Replace(List<ColorSlotKey> keys)
    {
        _colorKeys.Clear();
        foreach (var key in keys)
        {
            _colorKeys.Add(key);
        }
    }

    public bool Remove(ColorSlotKey key)
    {
        int found = _colorKeys.RemoveAll(k => k.Color == key.Color);
        return found > 0;
    }

    public bool Contains(ColorSlotKey key)
    {
        foreach (var k in _colorKeys)
        {
            if (k.Color == key.Color)
                return true;
        }

        return false;
    }
}
