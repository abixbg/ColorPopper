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
        foreach (var slot in ConnectedSlots)
        {
            await Task.Delay(200);
            slot.AutoOpenSlot();
        }
    }
}
