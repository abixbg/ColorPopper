using System;
using System.Collections.Generic;
using UnityEngine;
using Popper.Events;

public class ScoreController : ISlotStateChanged
{
    [SerializeField] private PlayerScoreData scoreData;

    private readonly EventBus _events;

    public ScoreController(EventBus events)
    {
        _events = events;
        _events.Subscribe<ISlotStateChanged>(this);
    }

    public PlayerScoreData ScoreData => scoreData;
    public event Action ScoreChanged;

    private void AddPointsLevel(int points)
    {
        scoreData = new PlayerScoreData(scoreData.Level + points, scoreData.Game);
        ScoreChanged?.Invoke();
    }

    #region ISlotStateChanged
    void ISlotStateChanged.OnSlotOpen(SlotData slot)
    {
        AddPointsLevel(3);
    }

    void ISlotStateChanged.OnSlotOpenAuto(SlotData slot)
    {
        AddPointsLevel(2);
    }

    void ISlotStateChanged.OnSlotBreak(SlotData slot)
    {
        AddPointsLevel(-5);
    }
    #endregion
}
