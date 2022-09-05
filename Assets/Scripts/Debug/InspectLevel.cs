using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectLevel : MonoBehaviour
{
    public Color acceptedColor;
    void Update()
    {
        if (GameManager.current.Level != null)
        {
            acceptedColor = GameManager.current.Level.AcceptedColor;
        }
    }
}
