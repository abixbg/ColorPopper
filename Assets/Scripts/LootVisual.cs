﻿using AGK.Core.EventBroadcast;
using Popper.Events;
using Popper.UI;
using System.Threading.Tasks;
using UnityEngine;

public class LootVisual : MonoBehaviour, ILootPicked
{
    private bool isActivated;

    private UIManager UI => GameManager.current.UiManager;
    private IEventBus Events => GameManager.current.Events;

    public SlotLoot LootData { get; set; }

    #region MonoBehaviour
    private void Start()
    {
        Events.Subscribe<ILootPicked>(this);
    }

    private void OnDestroy()
    {
        Events.Unsubscribe<ILootPicked>(this);
    }
    #endregion

    // destroys loot
    public void Break()
    {
        gameObject.SetActive(false);
    }

    private async Task MoveToCollectorAsync()
    {
        float speed = 10f;

        while (ReachedDestination())
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, UI.LootDestinationWorldPos, speed * Time.deltaTime);
            await Task.Yield();
        }
    }

    private bool ReachedDestination()
    {
        var dist = Vector2.Distance(gameObject.transform.position, UI.LootDestinationWorldPos);
        return dist > 0.1f;
    }

    async void ILootPicked.OnLootActivate(SlotLoot loot)
    {
        if (loot != LootData)
            return;

        await MoveToCollectorAsync();
        Despawn();
        Events.Broadcast<ILootConsumed>(sub => sub.OnLootConsumed(loot));
    }

    void ILootPicked.OnLootDiscard(SlotLoot loot)
    {
        if (loot != LootData)
            return;
        Break();
    }

    private void Despawn()
    {
        Events.Unsubscribe<ILootPicked>(this);
        Destroy(gameObject);
    }
}
