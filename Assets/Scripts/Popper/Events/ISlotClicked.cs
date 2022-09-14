using EventBroadcast;
using UnityEngine;

namespace Popper.Events
{
    public interface ISlotClicked : IEventSubscriber
    {
        void OnSlotClicked(Slot slot);
    }

    public interface ISlotStateChanged : IEventSubscriber
    {
        void OnSlotOpen(Slot slot);
        void OnSlotBreak(Slot slot);
    }
}