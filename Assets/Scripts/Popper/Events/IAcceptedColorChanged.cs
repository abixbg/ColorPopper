using EventBroadcast;
using UnityEngine;

namespace Popper.Events
{
    public interface IAcceptedColorChanged : IEventSubscriber
    {
        void OnAcceptedColorChange(Color color);
    }
}