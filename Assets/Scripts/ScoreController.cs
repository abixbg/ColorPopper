using System;
using System.Collections.Generic;
using UnityEngine;
using Popper.Events;

public class ScoreController : ILootConsumed
{
    [SerializeField] private PlayerScoreData scoreData;

    private readonly EventBus _events;

    public ScoreController(EventBus events)
    {
        _events = events;
        _events.Subscribe<ILootConsumed>(this);
    }

    public PlayerScoreData ScoreData => scoreData;
    public event Action ScoreChanged;

    private void AddPointsLevel(int points)
    {
        scoreData = new PlayerScoreData(scoreData.Level + points, scoreData.Game);
        ScoreChanged?.Invoke();
    }

    public void OnLootConsumed(Loot _)
    {
        Debug.Log($"[Score] added score={10}");
        AddPointsLevel(10);
    }
}
