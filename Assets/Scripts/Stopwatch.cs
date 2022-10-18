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
        _multiplier = 1f;
    }

    public void AddDelta(float deltaTime)
    {
        if (isActive)
        {
            _time += deltaTime * _multiplier;
            ValueUpdated?.Invoke();
        }
    }

    public void SetActive(bool state)
    {
        isActive = state;
    }

    public void Reset(float time = 0f)
    {
        _time = time;
    }
}
