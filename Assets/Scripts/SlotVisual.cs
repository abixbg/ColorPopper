using AGK.GameGrids;
using Popper.Events;
using UnityEngine;

[System.Serializable]
public class SlotVisual : MonoBehaviour, ISlotStateChanged
{
    [SerializeField] private Dot keyhole;
    [SerializeField] private LootVisual loot;
    [SerializeField] private SpriteRenderer border;

    public Dot Content { get => keyhole; set => keyhole = value; }

    public SlotData SlotData => grid.GetNodeAt(gridPos);

    private GameGrid2D<SlotData> grid;
    private GridPosition gridPos;

    private EventBus Events => GameManager.current.Events;

    public void Construct(GameGrid2D<SlotData> grid, GridPosition gridPos, Transform parent)
    {
        this.grid = grid;
        this.gridPos = gridPos;
        transform.parent = parent;

        Events.Subscribe<ISlotStateChanged>(this);
    }

    public void CmdClicked()
    {
        //Debug.Log($"Clicked! active={SlotData.IsActive}", gameObject);
        if (SlotData.IsActive == true)
        {
            Events.Broadcast<ISlotClicked>(s => s.OnSlotClicked(SlotData));
        }
    }

    //public async void OpenSlot()
    //{
    //    SlotData.IsActive = false;
    //    SlotData.IsLocked = false;

    //    Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpen(SlotData, this));
    //    UpdateVisual();

    //    // activate slot contents
    //    if (SlotData.Loot != null)
    //    {    
    //        //broadcast event
    //        Events.Broadcast<ILootPicked>(sub => sub.OnLootPicked(SlotData.Loot));
    //        await loot.Activate();
    //        Events.Broadcast<ILootConsumed>(sub => sub.OnLootConsumed(SlotData.Loot));
    //    }
    //}

    //public void BreakSlot()
    //{
    //    SlotData.IsActive = false;
    //    SlotData.IsLocked = true;

    //    Events.Broadcast<ISlotStateChanged>(s => s.OnSlotBreak(SlotData, this));
    //    UpdateVisual();

    //    //disable slot contents
    //    if (SlotData.Loot != null)
    //    {
    //        loot.Break();
    //    }      
    //}

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

    public void OnSlotOpen(SlotData data)
    {
        if (data == SlotData)
        {
            UpdateVisual();
        }
    }

    public void OnSlotBreak(SlotData data)
    {
        if (data == SlotData)
        {
            UpdateVisual();
        }
    }
}
