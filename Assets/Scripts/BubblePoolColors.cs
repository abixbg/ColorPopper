using UnityEngine;

public class BubblePoolColors
{
    private readonly ColorPalette _palette;

    public BubblePoolColors(ColorPalette palette)
    {
        _palette = palette;
    }

    public Color GetRandomColor()
    {
        int index = Random.Range(0, _palette.Colors.Count - 1);

        return _palette.Colors[index];
    }
}
