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

    public override Task ActivateEffect()
    {
        Debug.LogAssertion("[LootStar] NOT IMPLEMENTED!: Activate!"); //slot.SlotVisual.OpenSlot();
        return Task.CompletedTask;
    }
}
