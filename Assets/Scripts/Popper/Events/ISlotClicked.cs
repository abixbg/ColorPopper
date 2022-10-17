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
        void OnSlotOpenClick(SlotData slot);
        void OnSlotOpenAuto(SlotData slot);
        void OnSlotBreak(SlotData slot);
    }

    public interface ISlotVisualStateChanged : IEventSubscriber 
    {
        void OnDeactivated(SlotData slot);
        void OnBreak(SlotData slot);
    }
}