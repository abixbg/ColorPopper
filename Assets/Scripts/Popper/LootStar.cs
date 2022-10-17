using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LootStar : SlotLoot
{
    public LootStar(List<SlotData> connectedSlots)
    {
        ConnectedSlots = connectedSlots;
    }

    public List<SlotData> ConnectedSlots { get; }

    public override async Task ActivateEffect()
    {
        //instantly disable all connected
        foreach (var slot in ConnectedSlots)
        {
            slot.IsActive = false;
        }

        foreach (var slot in ConnectedSlots)
        {
            slot.AutoOpenSlot();
            await Task.Delay(200);
        }
    }
}
