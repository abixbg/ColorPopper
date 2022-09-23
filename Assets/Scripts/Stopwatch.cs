using System;

public class Stopwatch
{
    private float _time;
    private float _multiplier;
    public float TimeSec => _time;

    public event Action ValueUpdated;
    public Stopwatch(GameClock clock)
    {
        clock.TimeElapsedSec += AddDelta;
        _multiplier = 0.5f;
    }

    public void AddDelta(float deltaTime)
    {
        _time += deltaTime;
        ValueUpdated?.Invoke();
    }

    public void Reset()
    {
        _time = 0f;
    }
}
