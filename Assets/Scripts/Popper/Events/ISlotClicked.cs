using EventBroadcast;
using UnityEngine;

namespace Popper.Events
{
    public interface ISlotClicked : IEventSubscriber
    {
        void OnSlotClicked(SlotData slot);
    }

    public interface ISlotStateChanged : IEventSubscriber
    {
        void OnSlotOpen(SlotData data);
        void OnSlotBreak(SlotData data);
    }
}