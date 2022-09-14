using System;

namespace EventBroadcast
{
    public interface IEventBus
    {
        void Broadcast<T>(Action<T> callback) where T : class, IEventSubscriber;
        void Subscribe<T>(T subscriber) where T : IEventSubscriber;
        void Unsubscribe<T>(T subscriber) where T : IEventSubscriber;
    }
}
