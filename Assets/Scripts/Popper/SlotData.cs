using AGK.GameGrids;
using UnityEngine;

[System.Serializable]
public class SlotData : IGridCell, ICellContentMatch
{
    [SerializeField] private bool isActive;
    [SerializeField] private bool isLocked;
    [SerializeField] private GridPosition _gridPosition;

    public SlotContent Content { get; set; }

    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public bool IsActive { get => isActive; set => isActive = value; }

    [SerializeField] private SlotVisual slotVisual;
    public SlotVisual SlotVisual => slotVisual;
    GridPosition IGridCell.Position { get => _gridPosition; set => _gridPosition = value; }

    bool ICellContentMatch.IsMatch(ICellContentMatch other)
    {       
        return Content.IsMatch(other);
    }

    public void Init(bool isLocked, bool isActive)
    {
        this.isLocked = isLocked;
        this.isActive = isActive;
    }
}
