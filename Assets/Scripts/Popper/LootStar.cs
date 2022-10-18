using AGK.GameGrids;
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

        await Task.Delay(350);

        foreach (var slot in ConnectedSlots)
        {
            await Task.Delay(150);
            slot.AutoOpenSlot();
        }
    }
}
