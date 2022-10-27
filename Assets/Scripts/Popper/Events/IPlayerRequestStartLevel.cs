using EventBroadcast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerRequestLevel : IEventSubscriber
{
    void LevelLoad();
}
