using System;

public class Stopwatch
{
    private float _time;
    private float _multiplier;
    public float TimeSec => _time;

    private bool isActive;

    public event Action ValueUpdated;
    public Stopwatch(GameClock clock)
    {
        clock.TimeElapsedSec += AddDelta;
        _multiplier = 0.5f;
    }

    public void AddDelta(float deltaTime)
    {
        if (isActive)
        {
            _time += deltaTime;
            ValueUpdated?.Invoke();
        }
    }

    public void SetActive(bool state)
    {
        isActive = state;
    }

    public void Reset()
    {
        _time = 0f;
    }
}
