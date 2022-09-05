using Popper.UI;
using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

    public float timeFrom;
    float timeRemaining;

	// Use this for initialization
	void Start ()
    {
        timeRemaining = timeFrom;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UIManager.Instance.TopPanel.clockPanlel.timeText.text = FormatToSec(timeRemaining);
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
        timeRemaining += time;
    }
}
