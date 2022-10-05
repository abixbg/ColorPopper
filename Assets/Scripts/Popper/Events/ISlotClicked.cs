using EventBroadcast;
using UnityEngine;

namespace Popper.Events
{
    public interface ISlotClicked : IEventSubscriber
    {
        void OnSlotClicked(SlotVisual slot);
    }

    public interface ISlotStateChanged : IEventSubscriber
    {
        void OnSlotOpen(SlotVisual slot);
        void OnSlotBreak(SlotVisual slot);
    }
}