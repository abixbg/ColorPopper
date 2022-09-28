using Popper;
using UnityEngine;

[System.Serializable]
public struct ColorSlotKey : ISlotKeyData
{
    [SerializeField] private Color color;

    public Color Color => color;

    public ColorSlotKey(Color color)
    {
        this.color = color;
    }
}
