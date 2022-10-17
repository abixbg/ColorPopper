using EventBroadcast;
using UnityEngine;

namespace Popper.Events
{
    public interface ISlotInput : IEventSubscriber
    {
        void OnClicked(SlotData slot);
    }

    public interface ISlotStateChanged : IEventSubscriber
    {
        void OnSlotOpen(SlotData slot);
        void OnSlotOpenAuto(SlotData slot);
        void OnSlotBreak(SlotData slot);
    }

    public interface ISlotVisualStateChanged : IEventSubscriber 
    {
        void OnOpenSuccess(SlotData slot);
        void OnBreak(SlotData slot);
    }
}