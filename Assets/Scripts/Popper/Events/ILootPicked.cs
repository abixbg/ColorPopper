using EventBroadcast;

namespace Popper.Events
{
    public interface ILootPicked : IEventSubscriber
    {
        void OnLootActivate(SlotLoot loot);
        void OnLootDiscard(SlotLoot loot);
    }
}