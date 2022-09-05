using EventBroadcast;

namespace Popper.Events
{
    public interface ILootActivated : IEventSubscriber
    {
        void OnLootActivated(); //intent: will pass loot data
    }
}