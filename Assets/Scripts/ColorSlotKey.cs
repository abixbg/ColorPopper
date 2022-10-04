using Popper;
using UnityEngine;

[System.Serializable]
public struct ColorSlotKey : IMatchKey<ColorSlotKey>
{
    [SerializeField] private Color color;

    public Color Color => color;

    public ColorSlotKey(Color color)
    {
        this.color = color;
    }

    bool IMatchKey<ColorSlotKey>.IsMatch(ColorSlotKey other)
    {
        return color == other.Color;
    }
}
