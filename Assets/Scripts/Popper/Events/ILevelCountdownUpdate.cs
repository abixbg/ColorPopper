using AGK.Core.EventBroadcast;

namespace Popper.Events
{
    public interface ILevelCountdownUpdate : IEventSubscriber
    {
        void OnCountdownUpdate(int timeSeconds);
    }
}