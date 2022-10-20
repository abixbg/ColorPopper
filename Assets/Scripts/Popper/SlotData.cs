using AGK.GameGrids;
using Popper.Events;
using UnityEngine;

[System.Serializable]
public class SlotData : IGridCell, ICellContentMatch
{
    [SerializeField] private bool isActive;
    [SerializeField] private bool isLocked;
    [SerializeField] private GridPosition _gridPosition;

    public SlotContent Content { get; set; }
    public SlotLoot Loot { get; set; }
    public bool IsBroken { get => isLocked; set => isLocked = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    private EventBus Events => GameManager.current.Events;

    GridPosition IGridCell.Position { get => _gridPosition; set => _gridPosition = value; }

    bool ICellContentMatch.IsMatch(ICellContentMatch other)
    {
        //TODO: this needs improvement
        var otherSlot = ((SlotData)other).Content as ColorSlotKey;
        var thisSlot = Content as ColorSlotKey;

        bool match = otherSlot.Color == thisSlot.Color;
        //Debug.LogWarning($"[SlotData] other : {otherSlot.GetType().Name} --> {match}");

        return match;
    }

    public void OpenSlot()
    {
        IsActive = false;
        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpen(this));

        // activate slot contents
        if (Loot != null)
        {
            //broadcast event
            Events.Broadcast<ILootPicked>(s => s.OnLootActivate(Loot));
        }
    }

    public void AutoOpenSlot()
    {
        IsActive = false;
        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpenAuto(this));
    }

    public void BreakSlot()
    {
        IsActive = false;
        IsBroken = true;

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotBreak(this));

        //disable slot contents
        if (Loot != null)
        {
            Events.Broadcast<ILootPicked>(s => s.OnLootDiscard(Loot));
        }
    }
}
