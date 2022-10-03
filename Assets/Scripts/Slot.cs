using Popper.Events;
using UnityEngine;
using AGK.GameGrids;

[System.Serializable]
public class Slot : MonoBehaviour, IGridCell, ICellContentMatch
{
    public Dot Keyhole { get; set; }

    [SerializeField] private Loot loot;
    public Loot Loot { get => loot; set => loot = value; }

    public SpriteRenderer border;

    public bool IsLocked => data.IsLocked;
    public bool IsActive => data.IsActive;

    [SerializeField] private CellData data;

    private EventBus Events => GameManager.current.Events;

    GridPosition IGridCell.Position { get => data.Position; set => data.Position = value; }

    public void Construct(CellData data)
    {
        this.data = data;
    }

    public void CmdClicked()
    {
        if (IsActive == true)
        {
            Events.Broadcast<ISlotClicked>(s => s.OnSlotClicked(this));
        }
    }

    public void OpenSlot()
    {
        data.IsActive = false;
        data.IsLocked = false;


        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpen(this));

        // activate slot contents
        if (loot != null)
        {
            loot.Activate();
        }

        UpdateVisual();
    }

    public void BreakSlot()
    {
        data.IsActive = false;
        data.IsLocked = true;

        //disable slot contents
        if (loot != null)
        {
            loot.Break();
        }

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotBreak(this));
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (!data.IsActive)
        {
            if (data.IsLocked)
            {
                border.color = new Color32(140, 30, 30, 255);
            }
            else
            {
                border.enabled = false;
            }
        }
        else
            border.enabled = true;
        Keyhole.gameObject.SetActive(data.IsActive);
    }

    bool ICellContentMatch.IsMatch(ICellContentMatch other)
    {
        return ((Slot)other).Keyhole.Color == Keyhole.Color;
    }
}
