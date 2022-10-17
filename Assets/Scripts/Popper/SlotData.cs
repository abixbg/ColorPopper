using AGK.GameGrids;
using Popper.Events;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public class SlotData : IGridCell, ICellContentMatch
{
    [SerializeField] private bool isActive;
    [SerializeField] private bool isLocked;
    [SerializeField] private GridPosition _gridPosition;

    public SlotContent Content { get; set; }
    public SlotLoot Loot { get; set; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
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

    public void Init(bool isLocked, bool isActive)
    {
        this.isLocked = isLocked;
        this.isActive = isActive;
    }

    public async void OpenSlot()
    {
        IsActive = false;
        IsLocked = false;

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpenClick(this));

        // activate slot contents
        if (Loot != null)
        {
            //broadcast event
            Events.Broadcast<ILootPicked>(sub => sub.OnLootActivate(Loot));
        }
    }

    public async void AutoOpenSlot()
    {
        IsActive = false;
        IsLocked = false;

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpenAuto(this));
    }

    public void BreakSlot()
    {
        IsActive = false;
        IsLocked = true;       

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotBreak(this));

        //disable slot contents
        if (Loot != null)
        {
            Events.Broadcast<ILootPicked>(sub => sub.OnLootDiscard(Loot));
        }
    }
}
