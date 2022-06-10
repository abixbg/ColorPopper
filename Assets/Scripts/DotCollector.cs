using UnityEngine;
using System;

public class DotCollector : MonoBehaviour
{

    [SerializeField] private Color acceptedColor;
    public SpriteRenderer spriteColor;
    // Use this for initialization

    public Color AcceptedColor { get => acceptedColor; }

    public event Action AcceptedColorChanged;

    public void Init()
    {
        SwitchAcceptedColor();
    }

    public bool IsAcceptableColor(Color col)
    {
        bool foo = false;
        if (col == acceptedColor) foo = true;
        return foo;
    }

    public void SwitchAcceptedColor()
    {
        var current = acceptedColor;

        Debug.Log("pick new color collector");

        //get random color from the remaining in the grid
        acceptedColor = GameManager.current.currentGrid.GetRandomColor();

        //play sound when color changes
        if (acceptedColor != current)
        {
            //there is actual change of the color so notify the player
            SoundManager.current.collectorColorChange.Play();
            //set accepted color
            spriteColor.color = acceptedColor;
        }     

        AcceptedColorChanged?.Invoke();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dot")
        other.gameObject.SetActive(false);
    }
}
