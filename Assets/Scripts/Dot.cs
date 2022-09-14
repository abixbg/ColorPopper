using UnityEngine;
using System.Collections;

[System.Serializable]
public class Dot : MonoBehaviour
{
    // the color of the dot
    [SerializeField]
    Color dotColor;

    public Color color
    {
        get { return dotColor; }
    }


    //public int gridIndex;
    //public bool isInGrid;

    // the color of the sprite representing dot color 
    [SerializeField]
    SpriteRenderer colorSprite;


    // sets color of the dot
    public void SetColor(Color col)
    {
        dotColor = col;
        colorSprite.color = dotColor;
    }



}
