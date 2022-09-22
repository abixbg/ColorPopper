using System;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public event Action<float> TimeElapsedSec;

    void Update()
    {
        TimeElapsedSec?.Invoke(Time.deltaTime);
    }
}
