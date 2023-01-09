using AGK.Core.EventBroadcast;

namespace Popper.Events
{
    public interface IPlayerRequestLevel : IEventSubscriber
    {
        void OnLevelLoad();
        void OnLevelRetry();
    }
}