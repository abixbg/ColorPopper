using Popper;
using UnityEngine;

[System.Serializable]
public class Dot : MonoBehaviour, IMatchKey<ColorSlotKey>
{
    [SerializeField] private Color dotColor;
    [SerializeField] private SpriteRenderer colorSprite;
    public Color Color => dotColor;

    bool IMatchKey<ColorSlotKey>.IsMatch(ColorSlotKey other)
    {
        return other.Color == dotColor;
    }

    public void SetColor(Color col)
    {
        dotColor = col;
        colorSprite.color = dotColor;
    }
}
