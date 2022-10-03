using Popper.Events;
using Popper.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private EventBus _events;
    private UIManager _ui;
    private List<Slot> connectedSlots;

    public List<Slot> ConnectedSlots => connectedSlots;

    private bool isActivated;

    public void Construct(EventBus events, UIManager uiManager, List<Slot> connectedSlots)
    {
        _events = events;
        _ui = uiManager;
        this.connectedSlots = connectedSlots;
    }

    public async void Activate()
    {
        if (!isActivated)
        {
            //broadcast event
            _events.Broadcast<ILootPicked>(sub => sub.OnLootPicked());
            await MoveToCollectorAsync();
            _events.Broadcast<ILootConsumed>(sub => sub.OnLootConsumed(this));

            isActivated = true;
        }
    }

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
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _ui.LootDestinationWorldPos, speed * Time.deltaTime);
            await Task.Yield();
        }
    }

    private bool ReachedDestination()
    {
        var dist = Vector2.Distance(gameObject.transform.position, _ui.LootDestinationWorldPos);
        return dist > 0.2f;
    }
}
