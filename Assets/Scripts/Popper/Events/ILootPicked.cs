using EventBroadcast;

namespace Popper.Events
{
    public interface ILootPicked : IEventSubscriber
    {
        void OnLootPicked();
    }
}