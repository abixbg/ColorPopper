using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIClockPanel : MonoBehaviour
{
    public Text timeText;
    public Text elapsedText;

    private Countdown _source;
    private Stopwatch _stopwatch;

    private void OnDestroy()
    {
        //_source.ValueUpdated -= OnSourceDataUpdate;
        //_stopwatch.ValueUpdated -= OnStopwatchDataUpdate;
    }

    public void Construct(Countdown source, Stopwatch stopwatch)
    {
        _source = source;
        _source.ValueUpdated += OnSourceDataUpdate;

        _stopwatch = stopwatch;
        _stopwatch.ValueUpdated += OnStopwatchDataUpdate;
    }

    private void OnSourceDataUpdate()
    {
        UpdateUI(FormatToSec(_source.TimeRemaining));
    }

    private void OnStopwatchDataUpdate()
    {
        UpdateStopwatch(string.Format("{0:F2}", _stopwatch.TimeSec));
    }

    private void UpdateStopwatch(string text)
    {
        elapsedText.text = text;
    }

    private void UpdateUI(string text)
    {
        timeText.text = text;
    }

    public static string FormatToSec(float time)
    {
        int roundedToInt = (int)time; //TODO: fix rounding
        string formatedTimeString = roundedToInt.ToString();
        return formatedTimeString;
    }
}
