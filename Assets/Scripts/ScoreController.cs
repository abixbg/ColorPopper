using System;
using System.Collections.Generic;
using UnityEngine;
using Popper.Events;
using EventBroadcast;

public class ScoreController : ISlotStateChanged
{
    [SerializeField] private PlayerScoreData scoreData;

    private IEventBus Events => GameManager.current.Events;

    public ScoreController()
    {
        Events.Subscribe<ISlotStateChanged>(this);
        Events.Broadcast<IPlayerScoreChanged>(s => s.OnUpdatePlayerScoreData(scoreData));
    }

    public PlayerScoreData ScoreData => scoreData;

    private void AddPointsLevel(int points)
    {
        scoreData = new PlayerScoreData(scoreData.LevelPoints + points, scoreData.Game);
        Events.Broadcast<IPlayerScoreChanged>(s => s.OnUpdatePlayerScoreData(scoreData));
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
