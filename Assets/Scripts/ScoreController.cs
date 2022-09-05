using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{
    [SerializeField] private PlayerScoreData scoreData;

    public PlayerScoreData ScoreData => scoreData;
    public event Action ScoreChanged;

    public void AddPointsLevel(int points)
    {
        scoreData = new PlayerScoreData(scoreData.Level + points, scoreData.Game);
        ScoreChanged?.Invoke();
    }
}
