using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootStar : SlotLoot
{
    public LootStar(List<SlotData> connectedSlots)
    {
        ConnectedSlots = connectedSlots;
    }

    public List<SlotData> ConnectedSlots { get; set; }

    public override void Activate()
    {
        Debug.LogError("Star Activated");
    }
}
