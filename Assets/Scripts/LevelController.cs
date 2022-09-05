using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController
{
    private Color _acceptedColor;
    private readonly DotCollector collector;

    public Color AcceptedColor { get => _acceptedColor; }

    public event Action AcceptedColorChanged;
    public LevelController(DotCollector collector)
    {
        this.collector = collector;
    }

    public void StartLevel()
    {
        SwitchAcceptedColor();
    }

    public void SwitchAcceptedColor()
    {
        var current = _acceptedColor;

        Debug.Log("pick new color collector");

        //get random color from the remaining in the grid
        _acceptedColor = GameManager.current.currentGrid.GetRandomColor();

        //play sound when color changes
        if (_acceptedColor != current)
        {
            //there is actual change of the color so notify the player
            SoundManager.current.collectorColorChange.Play();
            //set accepted color
            collector.spriteColor.color = _acceptedColor;
        }

        AcceptedColorChanged?.Invoke();
    }

    public bool IsAcceptableColor(Color col)
    {
        bool foo = false;
        if (col == _acceptedColor) foo = true;
        return foo;
    }
}
