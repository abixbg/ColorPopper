using Popper.Events;
using Popper.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private bool isActivated;

    private UIManager UI => GameManager.current.UiManager;

    public async Task Activate()
    {
        if (!isActivated)
        {
            await MoveToCollectorAsync();
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
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, UI.LootDestinationWorldPos, speed * Time.deltaTime);
            await Task.Yield();
        }
    }

    private bool ReachedDestination()
    {
        var dist = Vector2.Distance(gameObject.transform.position, UI.LootDestinationWorldPos);
        return dist > 0.2f;
    }
}
