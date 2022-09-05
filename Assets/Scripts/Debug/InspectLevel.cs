using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class InspectLevel : MonoBehaviour
{
    public Color acceptedColor;

    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        if (_gameManager.Level != null)
        {
            acceptedColor = _gameManager.Level.AcceptedColor;
        }
    }
}
