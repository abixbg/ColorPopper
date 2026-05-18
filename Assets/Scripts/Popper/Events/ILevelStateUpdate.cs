using AGK.EventBroadcast;

namespace Popper.Events
{
    public interface ILevelStateUpdate : IEventSubscriber
    {
        void OnLevelStartGenerating();
        void OnLevelCompleted();
        void OnLevelFinalScore(PlayerScoreData scoreData);
    }
}