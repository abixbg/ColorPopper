using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIClockPanel : MonoBehaviour
{
    public Text timeText;

    private Countdown _source;

    private void OnDestroy()
    {
        _source.ValueUpdated -= OnSourceDataUpdate;
    }

    public void Construct(Countdown source)
    {
        _source = source;
        _source.ValueUpdated += OnSourceDataUpdate;
    }

    private void OnSourceDataUpdate()
    {
        UpdateUI(FormatToSec(_source.TimeRemaining));
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
