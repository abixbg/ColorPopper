using UnityEngine;
using System.Collections;

public class ColorPalette : MonoBehaviour
{

    //public int size;
    public Color[] paletteColor;

    public Color GetRandomColor()
    {
        int index = Random.Range(0, paletteColor.Length);

        return paletteColor[index];
    }
}
