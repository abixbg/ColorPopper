using EventBroadcast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Popper.Events
{
    public interface ILevelStateUpdate : IEventSubscriber
    {
        void OnLevelCompleted();
    }
}