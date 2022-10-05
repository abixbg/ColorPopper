using Popper.Events;
using UnityEngine;
using AGK.GameGrids;

[System.Serializable]
public class Slot : MonoBehaviour
{  
    public Dot Keyhole { get; set; }

    [SerializeField] private Loot loot;
    public Loot Loot { get => loot; set => loot = value; }

    public SpriteRenderer border;

    [SerializeField] private SlotData Data => grid.GetNodeAt(gridPos);

    private GameGrid2D<SlotData> grid;
    private GridPosition gridPos;

    private EventBus Events => GameManager.current.Events;

    //GridPosition IGridCell.Position { get => data.Position; set => data.Position = value; }

    public void Construct(GameGrid2D<SlotData> grid, CellData posData)
    {
        this.grid = grid;
        this.gridPos = posData.Position;
    }

    public void CmdClicked()
    {
        Debug.LogError($"Clicked! active={Data.IsActive}", gameObject);
        if (Data.IsActive == true)
        {
            Events.Broadcast<ISlotClicked>(s => s.OnSlotClicked(this));
        }
    }

    public void OpenSlot()
    {
        Data.IsActive = false;
        Data.IsLocked = false;


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
        Data.IsActive = false;
        Data.IsLocked = true;

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
        if (!Data.IsActive)
        {
            if (Data.IsLocked)
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
        Keyhole.gameObject.SetActive(Data.IsActive);
    }

    //bool ICellContentMatch.IsMatch(ICellContentMatch other)
    //{
    //    return ((Slot)other).Keyhole.Color == Keyhole.Color;
    //}
}
