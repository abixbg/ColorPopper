using Popper.Events;
using Popper.UI;
using System.Threading.Tasks;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private EventBus _events;
    private UIManager _ui;

    public void Construct(EventBus events, UIManager uiManager)
    {
        _events = events;
        _ui = uiManager;
    }

    public async void Activate()
    {
        Debug.LogError("Started");
        //broadcast event
        _events.Broadcast<ILootPicked>(sub => sub.OnLootPicked());

        await MoveToCollectorAsync();

        Debug.LogError("Finished");
        _events.Broadcast<ILootConsumed>(sub => sub.OnLootConsumed());
    }

    // destroys loot
    public void Break()
    {
        gameObject.SetActive(false);
    }

    private async Task MoveToCollectorAsync()
    {
        float speed = 8f;

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
