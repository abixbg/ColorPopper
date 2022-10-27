using EventBroadcast;
using System;
using System.Collections.Generic;

namespace Popper.Events
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<IEventSubscriber>> _subscribers;

        public EventBus()
        {
            _subscribers = new Dictionary<Type, List<IEventSubscriber>>();
        }

        public void Subscribe<T>(T subscriber) where T : IEventSubscriber
        {
            Type key = typeof(T);
            if (!_subscribers.ContainsKey(key))
            {
                _subscribers.Add(key, new List<IEventSubscriber> { subscriber });
            }
            else
            {
                if (_subscribers[key].Contains(subscriber))
                {
                    UnityEngine.Debug.LogAssertion($"Already subscribed {key.Name}");
                    return;
                }
                _subscribers[key].Add(subscriber);
            }
        }

        public void Unsubscribe<T>(T subscriber) where T : IEventSubscriber
        {
            Type key = typeof(T);
            if (_subscribers.ContainsKey(key))
                _subscribers[key].Remove(subscriber);
        }

        public void Broadcast<T>(Action<T> callback, bool log = false) where T : class, IEventSubscriber
        {
            Type key = typeof(T);

            if (_subscribers.ContainsKey(key))
            {
                List<IEventSubscriber> subscribers = _subscribers[key];

                foreach (var subscriber in subscribers)
                {
                    try
                    {
                        callback.Invoke(subscriber as T);
                        if (log)
                            UnityEngine.Debug.Log($"EVENT --> [{typeof(T).Name}]");                       
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogAssertion(e);
                    }
                }
            }
        }
    }
}