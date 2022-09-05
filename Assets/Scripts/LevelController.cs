using System;
using System.Collections.Generic;
using UnityEngine;
using Popper.Events;

public class LevelController : ILootActivated
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

    public LevelController(LevelConfigData levelData, EventBus levelEvents)
    {
        _levelData = levelData;

        countdown = new Countdown(levelData.TimeSec);
        levelEvents.LootActivated += OnLootActivated;
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

    public void OnLootActivated()
    {


        //Always Switch Color on Loot activation
        GameManager.current.Level.SwitchAcceptedColor();

        Debug.Log("[Level] AddTime!");
        countdown.AddTime(3);

        //switches colorcollector by % chance
        if (UnityEngine.Random.value <= 0.3f)
        {
            //Color nextColor = GameManager.current.currentGrid.colorPalette.GetRandomColor();
            GameManager.current.Level.SwitchAcceptedColor();
        }
    }
}
