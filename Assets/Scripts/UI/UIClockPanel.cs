using AGK.Core.EventBroadcast;
using Popper.Events;
using UnityEngine;
using UnityEngine.UI;

public class UIClockPanel : MonoBehaviour, ILevelStopwatchUpdate
{
    public Text timeText;
    public Text elapsedText;

    private IEventBus Events => GameManager.current.Events;

    public void Construct()
    {
        Events.Subscribe<ILevelStopwatchUpdate>(this);
    }

    void ILevelStopwatchUpdate.OnValueUpdate(LevelTimeData data)
    {
        timeText.text = string.Format("{0:F0}", data.Remaining);
        elapsedText.text = string.Format("{0:F2}", data.Elapsed);
    }

    void ILevelStopwatchUpdate.OnReset()
    {
        throw new System.NotImplementedException();
    }
}
