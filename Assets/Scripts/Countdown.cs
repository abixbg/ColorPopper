using Popper.UI;
using System;

public class Countdown
{
    private float _timeRemaining;
    public Countdown(int initialSec)
    {
        _timeRemaining = initialSec;
    }

    public float TimeRemaining => _timeRemaining;

    public event Action ValueUpdated;

    public void ConsumeTime(float deltaTime)
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= deltaTime;
            ValueUpdated?.Invoke();
        }
    }


    public void AddTime(float time)
    {
        _timeRemaining += time;
    }
}
