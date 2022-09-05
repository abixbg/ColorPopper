using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController
{
    private LevelConfigData _levelData;
    private Color _acceptedColor;
    private Countdown countdown;

    public Color AcceptedColor { get => _acceptedColor; }
    public LevelConfigData Config => _levelData;
    public Countdown Countdown => countdown;
    

    public float TimeRemaining => countdown.TimeRemaining;

    public event Action AcceptedColorChanged;
    public event Action LevelPhaseChanged; //NOTE: for now only called once on level start 

    public LevelController(LevelConfigData levelData)
    {
        _levelData = levelData;

        countdown = new Countdown(levelData.TimeSec);
    }

    public void SetPhaseInitialize()
    {
        Debug.Log("[Level] Initialilze!");
        LevelPhaseChanged?.Invoke();
    }

    public void StartLevel()
    {
        Debug.Log("[Level] Start!");
        SwitchAcceptedColor();
    }

    public void SwitchAcceptedColor()
    {
        var current = _acceptedColor;

        //get random color from the remaining in the grid
        _acceptedColor = GameManager.current.currentGrid.GetRandomColor();

        //play sound when color changes
        if (_acceptedColor != current)
        {
            //there is actual change of the color so notify the player
            SoundManager.current.collectorColorChange.Play();
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
