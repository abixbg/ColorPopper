using EventBroadcast;

namespace Popper.Events
{
    public interface ILevelStopwatchUpdate : IEventSubscriber
    {
        void OnValueUpdate(LevelTimeData data);
        void OnReset();
    }
}