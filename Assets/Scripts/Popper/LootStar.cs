using System.Collections.Generic;
using UnityEngine;

public class LootStar : SlotLoot
{
    public LootStar(List<SlotData> connectedSlots)
    {
        ConnectedSlots = connectedSlots;
    }

    public List<SlotData> ConnectedSlots { get; }

    public override void Activate()
    {
        Debug.LogAssertion("[LootStar] NOT IMPLEMENTED!: Activate"); //slot.SlotVisual.OpenSlot();
    }
}
