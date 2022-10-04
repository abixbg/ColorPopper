using AGK.GameGrids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData : IGridCell, ICellContentMatch
{
    [SerializeField] private CellData data;
    public bool IsActive => slotVisual.IsActive;

    [SerializeField] private Slot slotVisual;
    public Slot SlotVisual => slotVisual;

    GridPosition IGridCell.Position { get => data.Position; set => data.Position = value; }

    bool ICellContentMatch.IsMatch(ICellContentMatch other)
    {
        return ((SlotData)other).SlotVisual.Keyhole.Color == SlotVisual.Keyhole.Color;
    }   

    public void SetData(CellData data)
    {
        this.data = data;
    }

    public void SetVisual(Slot slot)
    {
        slotVisual = slot;
    }
}
