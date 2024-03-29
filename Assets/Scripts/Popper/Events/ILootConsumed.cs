using AGK.Core.EventBroadcast;

namespace Popper.Events
{
    public interface ILootConsumed : IEventSubscriber
    {
        void OnLootConsumed(SlotLoot loot);
    }
}