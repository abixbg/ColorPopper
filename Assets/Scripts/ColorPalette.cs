using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "colorPallete", menuName = "Assets/palete")]
public class ColorPalette : ScriptableObject
{
    [SerializeField] private List<Color> paletteColors;

    public List<Color> Colors { get => paletteColors; }
}
