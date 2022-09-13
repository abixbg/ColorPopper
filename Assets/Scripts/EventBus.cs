using System;

//TODO: replace this with proper event broadcast system
public class EventBus
{
    public event Action LootActivated;
    public event Action LootConsumed;

    public void InvokeLootActivated()
    {
        LootActivated?.Invoke();
    }

    public void InvokeLootConsumed()
    {
        LootConsumed?.Invoke();
    }

    public void SubscribeLootActivated(Action callback)
    {
        LootActivated += callback;
    }

    public void UnubscribeLootActivated(Action callback)
    {
        LootActivated -= callback;
    }
}
