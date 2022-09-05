using Popper.UI;
using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    private EventBus _events;

    public void Construct(EventBus events)
    {
        _events = events;
    }

    public void Activate()
    {
        // activete pop sound
        //int popSoundId = Random.Range(0, SoundManager.current.dotPop.Length - 1);
        //SoundManager.current.dotPop[popSoundId].Play();
        SoundManager.current.LootActivate.Play();

        // move graphic to the collector
        StartCoroutine(MoveTooCollector());

        //broadcast event
        _events.InvokeLootActivated();
    }

    // destroys loot
    public void Break()
    {
        gameObject.SetActive(false);
    }

    IEnumerator MoveTooCollector()
    {
        float speed = 15f;
        while (gameObject.transform.position != UIManager.Instance.LootDestinationWorldPos)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, UIManager.Instance.LootDestinationWorldPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
