using AGK.GameGrids;
using Popper;
using UnityEngine;

[System.Serializable]
public class ColorSlotKey : SlotContent
{
    [SerializeField] private Color color;

    public Color Color => color;

    public ColorSlotKey(Color color)
    {
        this.color = color;
    }

    public override bool IsMatch(ICellContentMatch other)
    {
        if (other is ColorSlotKey)
        {
            return color == ((ColorSlotKey)other).Color;
        }
        else
            return false;
    }
}
