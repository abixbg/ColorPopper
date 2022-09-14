using System;
using System.Collections.Generic;
using UnityEngine;
using Popper.Events;
using EventBroadcast;

public class LevelController : ILootPicked, ISlotClicked, ISlotStateChanged
{
    private LevelConfigData _levelData;
    private Color _acceptedColor;
    private readonly Countdown _countdown;

    public Color AcceptedColor { get => _acceptedColor; }
    public LevelConfigData Config => _levelData;
    public Countdown Countdown => _countdown;

    public float TimeRemaining => _countdown.TimeRemaining;

    private IEventBus _events;
    private Board _board;

    //public event Action AcceptedColorChanged;
    //public event Action LevelPhaseChanged; //NOTE: for now only called once on level start 

    public LevelController(LevelConfigData levelData, EventBus levelEvents)
    {
        _levelData = levelData;
        _events = levelEvents;
        _countdown = new Countdown(levelData.TimeSec);
        
        _events.Subscribe<ILootPicked>(this);
        _events.Subscribe<ISlotClicked>(this);
        _events.Subscribe<ISlotStateChanged>(this);
    }

    public void SetPhaseInitialize(Board board)
    {
        _board = board;
        _board.SetInitialState();
        Debug.Log("[Level] Initialilze!");
    }

    public void StartLevel()
    {
        Debug.Log("[Level] Start!");
        SwitchAcceptedColor();
    }

    private void SwitchAcceptedColor()
    {
        //get random color from the remaining in the grid
        _acceptedColor = GameManager.current.Board.GetRandomColor();
        _events.Broadcast<IAcceptedColorChanged>(s => s.OnAcceptedColorChange(_acceptedColor));
    }

    public bool IsAcceptableColor(Color col)
    {
        bool foo = false;
        if (col == _acceptedColor) foo = true;
        return foo;
    }

    void ILootPicked.OnLootPicked()
    {
        //Always Switch Color on Loot activation
        SwitchAcceptedColor();

        Debug.Log("[Level] AddTime!");
        _countdown.AddTime(3);
    }

    void ISlotClicked.OnSlotClicked(Slot slot)
    {
        if (Accepted(slot))
            slot.OpenSlot();
        else
            slot.BreakSlot();

        //switches colorcollector by % chance
        if (UnityEngine.Random.value <= 0.3f)
        {
            SwitchAcceptedColor();
        }
    }

    public bool Accepted(Slot slot)
    {
        if (GameManager.current.Level.IsAcceptableColor(slot.Keyhole.color) == true)
        {
            return true;
        }
        else return false;
    }

    void ISlotStateChanged.OnSlotOpen(Slot slot)
    {
        ValidateBoard();
    }

    void ISlotStateChanged.OnSlotBreak(Slot slot)
    {
        ValidateBoard();
    }

    private void ValidateBoard()
    {
        GameManager.current.Board.UpdateColorList();
        if (!GameManager.current.Board.DotColors.Contains(_acceptedColor))
        {
            SwitchAcceptedColor();
        }
    }
}
