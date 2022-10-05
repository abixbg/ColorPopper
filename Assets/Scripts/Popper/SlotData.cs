using AGK.GameGrids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData : IGridCell, ICellContentMatch
{
    [SerializeField] private bool isActive;
    [SerializeField] private bool isLocked;
    [SerializeField] private GridPosition _gridPosition;

    //private ICellContentMatch keyhole;

    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public bool IsActive { get => isActive; set => isActive = value; }

    [SerializeField] private Slot slotVisual;
    public Slot SlotVisual => slotVisual;

    GridPosition IGridCell.Position { get => _gridPosition; set => _gridPosition = value; }

    bool ICellContentMatch.IsMatch(ICellContentMatch other)
    {
        return ((SlotData)other).SlotVisual.Keyhole.Color == SlotVisual.Keyhole.Color;
    }

    public void SetAdditionalData(bool isLocked, bool isActive)
    {
        this.isLocked = isLocked;
        this.isActive = isActive;
    }

    public void SetVisual(Slot slot)
    {
        slotVisual = slot;
    }
}
