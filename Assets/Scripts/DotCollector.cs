using UnityEngine;
using System.Collections;

public class DotCollector : MonoBehaviour
{

    public Color acceptedColor;
    public SpriteRenderer spriteColor;
    // Use this for initialization




    public bool IsAcceptableColor(Color col)
    {
        bool foo = false;
        if (col == acceptedColor) foo = true;
        return foo;
    }

    



    public void SwitchAcceptedColor()
    {
        Debug.Log("set new color collector");

        //play sound when color changes
        SoundManager.current.collectorColorChange.Play();

        //get random color from the remaining in the grid
        acceptedColor = GameManager.current.currentGrid.GetRandomColor();

        //set accepted color
        spriteColor.color = acceptedColor;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dot")
        other.gameObject.SetActive(false);
    }
}
