using UnityEngine;

[System.Serializable]
public class Dot : MonoBehaviour
{
    [SerializeField] private Color dotColor;
    [SerializeField] private SpriteRenderer colorSprite;

    public Color Color => dotColor;

    public void SetColor(Color col)
    {
        dotColor = col;
        colorSprite.color = dotColor;
    }
}
