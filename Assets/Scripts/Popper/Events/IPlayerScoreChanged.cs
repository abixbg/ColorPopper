using AGK.Core.EventBroadcast;

namespace Popper.Events
{
    public interface IPlayerScoreChanged : IEventSubscriber
    {
        void OnUpdatePlayerScoreData(PlayerScoreData data);
    }
}