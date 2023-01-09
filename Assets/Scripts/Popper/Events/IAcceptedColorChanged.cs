using AGK.Core.EventBroadcast;

namespace Popper.Events
{
    public interface IAcceptedColorChanged : IEventSubscriber
    {
        void OnAcceptedColorChange(SlotContent key);
    }
}