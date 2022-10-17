using System;
using System.Collections.Generic;
using UnityEngine;
using Popper.Events;

public class ScoreController : ILootPicked
{
    [SerializeField] private PlayerScoreData scoreData;

    private readonly EventBus _events;

    public ScoreController(EventBus events)
    {
        _events = events;
        _events.Subscribe<ILootPicked>(this);
    }

    public PlayerScoreData ScoreData => scoreData;
    public event Action ScoreChanged;

    private void AddPointsLevel(int points)
    {
        scoreData = new PlayerScoreData(scoreData.Level + points, scoreData.Game);
        ScoreChanged?.Invoke();
    }

    void ILootPicked.OnLootActivate(SlotLoot loot)
    {
        Debug.Log($"[Score] added score={10}");
        AddPointsLevel(10);
    }

    void ILootPicked.OnLootDiscard(SlotLoot _)
    {

    }
}
