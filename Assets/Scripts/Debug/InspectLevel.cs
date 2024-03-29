using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class InspectLevel : MonoBehaviour
{
    public bool enableUpdate = false;
    public ColorSlotKey acceptedColor;
    public float time;

    public List<ColorSlotKey> AllColors;
    public int RemainingSlots;

    public List<SlotData> Slots;

    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        if (_gameManager.Level != null && enableUpdate)
        {
            acceptedColor = _gameManager.Level.AcceptRules.Current;
            time = _gameManager.Level.TimeRemaining;

            AllColors = _gameManager.Level.KeyPool.Pool;
            RemainingSlots = _gameManager.Level.BoardCellremaining;

            Slots = _gameManager.Level.Grid.Nodes;
        }
    }
}
