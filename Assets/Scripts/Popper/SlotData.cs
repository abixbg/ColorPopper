using AGK.GameGrids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData : IGridCell, ICellContentMatch
{
    private CellData data;
    public bool IsActive => data.IsActive;

    [SerializeField] private Slot slotVisual;
    public Slot SlotVisual => slotVisual;

    GridPosition IGridCell.Position { get => data.Position; set => data.Position = value; }

    bool ICellContentMatch.IsMatch(ICellContentMatch other)
    {
        var thisCell = ((SlotData)other);

        if (thisCell.SlotVisual.Keyhole == null)
        {
            Debug.LogError("ITS NULL!!", thisCell.SlotVisual.gameObject);
        }

        return thisCell.SlotVisual.Keyhole.Color == SlotVisual.Keyhole.Color;
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
