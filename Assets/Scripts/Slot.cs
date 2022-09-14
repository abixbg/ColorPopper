using Popper.Events;
using UnityEngine;

[System.Serializable]
public class Slot : MonoBehaviour
{
    public Dot Keyhole { get; set; }

    [SerializeField] private Loot loot;
    public Loot Loot { get => loot; set => loot = value; }

    public int index;

    public SpriteRenderer border;

    public bool isLocked;
    public bool isActive;

    private EventBus Events => GameManager.current.Events;

    void Awake()
    {
        isActive = true;
    }

    public void CmdClicked()
    {
        if (isActive == true)
        {
            Events.Broadcast<ISlotClicked>(s => s.OnSlotClicked(this));
        }
    }

    public void OpenSlot()
    {
        isActive = false;
        isLocked = false;

        // update slot graphics to unlocked state
        border.enabled = false;

        // activate slot contents
        if (loot != null)
        {
            loot.Activate();
        }

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotOpen(this));
        HideSlot();
    }

    public void BreakSlot()
    {
        isActive = false;
        isLocked = true;

        //update graphics to broken state
        border.color = new Color32(140, 30, 30, 255);

        //disable slot contents
        if (loot != null)
        {
            loot.Break();
        }

        Events.Broadcast<ISlotStateChanged>(s => s.OnSlotBreak(this));
        HideSlot();
    }

    private void HideSlot()
    {
        //disable dot
        Keyhole.gameObject.SetActive(false);
    }
}
