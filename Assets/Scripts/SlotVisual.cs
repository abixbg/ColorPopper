using Popper.Events;
using UnityEngine;
using AGK.GameGrids;
using UnityEditor.PackageManager;

[System.Serializable]
public class SlotVisual : MonoBehaviour
{
    [SerializeField] private Dot keyhole;
    [SerializeField] private Loot loot;
    [SerializeField] private SpriteRenderer border;

    public Dot Content { get => keyhole; set => keyhole = value; }

    public Loot Loot { get => loot; set => loot = value; }


    public SlotData SlotData => grid.GetNodeAt(gridPos);

    private GameGrid2D<SlotData> grid;
    private GridPosition gridPos;

    private EventBus Events => GameManager.current.Events;

    public void Construct(GameGrid2D<SlotData> grid, GridPosition gridPos, Transform parent)
    {
        this.grid = grid;
        this.gridPos = gridPos;
        transform.parent = parent;
    }

    public void CmdClicked()
    {
        //Debug.Log($"Clicked! active={SlotData.IsActive}", gameObject);
        if (SlotData.IsActive == true)
        {
            Events.Broadcast<ISlotClicked>(s => s.OnSlotClicked(this));
        }
    }

    public async void OpenSlot()
    {
        SlotData.IsActive = false;
        SlotData.IsLocked = false;


        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpen(SlotData, this));

        // activate slot contents
        if (SlotData.Loot != null)
        {    
            //broadcast event
            Events.Broadcast<ILootPicked>(sub => sub.OnLootPicked(SlotData.Loot));
            SlotData.Loot.Activate();
            await loot.Activate();
            Events.Broadcast<ILootConsumed>(sub => sub.OnLootConsumed(SlotData.Loot));
        }

        UpdateVisual();
    }

    public void BreakSlot()
    {
        SlotData.IsActive = false;
        SlotData.IsLocked = true;

        //disable slot contents
        if (SlotData.Loot != null)
        {
            loot.Break();
        }

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotBreak(SlotData, this));
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (!SlotData.IsActive)
        {
            if (SlotData.IsLocked)
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
        Content.gameObject.SetActive(SlotData.IsActive);
    }

    //bool ICellContentMatch.IsMatch(ICellContentMatch other)
    //{
    //    return ((Slot)other).Keyhole.Color == Keyhole.Color;
    //}
}
