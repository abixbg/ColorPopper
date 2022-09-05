using Popper.UI;

public class Countdown
{
    private float _timeRemaining;
    public Countdown(int initialSec)
    {
        _timeRemaining = initialSec;
    }

    public void OnGameClockUpdate(float deltaTime)
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= deltaTime;

            //TODO: Emit message that timer value changed (clock UI should be subscribed)
            UIManager.Instance.TopPanel.clockPanlel.timeText.text = FormatToSec(_timeRemaining);
        }
    }

    string FormatToSec(float time)
    {
        int roundedToInt = (int)time;
        string formatedTimeString = roundedToInt.ToString();
        return formatedTimeString;
    }

    public void AddTime(float time)
    {
        _timeRemaining += time;
    }
}
