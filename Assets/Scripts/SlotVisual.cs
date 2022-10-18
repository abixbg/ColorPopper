using AGK.GameGrids;
using Popper.Events;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class SlotVisual : MonoBehaviour, ISlotStateChanged
{
    [SerializeField] private Dot keyhole;
    [SerializeField] private LootVisual loot;
    [SerializeField] private SpriteRenderer border;
    [SerializeField] private AnimationCurve animCurve;

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

    public async Task Spawn()
    {
        await AnimateScale(0.2f, 1f, 25f);
    }

    public async Task Despawn()
    {
        await AnimateScale(1, 0.2f, 25f);

        Events.Unsubscribe<ISlotStateChanged>(this);
        Destroy(gameObject);
    }

    public void CmdClicked()
    {
        Debug.Log($"Clicked! active={SlotData.IsActive}", gameObject);
        if (SlotData.IsActive == true)
        {
            Events.Broadcast<ISlotInput>(s => s.OnClicked(SlotData));
        }
    }

    private void UpdateVisual()
    {
        if (!SlotData.IsActive)
        {
            if (SlotData.IsBroken)
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



    void ISlotStateChanged.OnSlotOpen(SlotData slot)
    {
        if (SlotData != slot)
            return;

        Events.Broadcast<ISlotVisualStateChanged>(e => e.OnOpenSuccess(SlotData));
        UpdateVisual();
    }

    void ISlotStateChanged.OnSlotBreak(SlotData slot)
    {
        if (SlotData != slot)
            return;

        Events.Broadcast<ISlotVisualStateChanged>(e => e.OnBreak(SlotData));
        UpdateVisual();
    }

    async void ISlotStateChanged.OnSlotOpenAuto(SlotData slot)
    {
        if (SlotData != slot)
            return;

        if (slot.IsBroken)
            return;
        
        await AnimateScale(1, 0.2f, 6f);
        Events.Broadcast<ISlotVisualStateChanged>(e => e.OnOpenSuccess(SlotData));
        UpdateVisual();
    }

    private async Task AnimateScale(float fromScale, float toScale, float speed)
    {
        bool done = false;

        float time = 0;
        Vector3 from = new Vector3(fromScale, fromScale, fromScale);
        Vector3 to = new Vector3(toScale, toScale, toScale);

        while (!done)
        {
            gameObject.transform.localScale = Vector3.Lerp(from, to, animCurve.Evaluate(time));
            time += Time.deltaTime * speed;

            if (time >= 1)
                done = true;
            await Task.Yield();
        }
    }
}
