using Popper.UI;
using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    private EventBus _events;
    private UIManager _ui;

    public void Construct(EventBus events, UIManager uiManager)
    {
        _events = events;
        _ui = uiManager;
    }

    public void Activate()
    {
        //broadcast event
        _events.InvokeLootActivated();

        // move graphic to the collector
        StartCoroutine(MoveTooCollector());
    }

    // destroys loot
    public void Break()
    {
        gameObject.SetActive(false);
    }

    IEnumerator MoveTooCollector()
    {
        float speed = 15f;
        while (gameObject.transform.position !=_ui.LootDestinationWorldPos)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _ui.LootDestinationWorldPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
