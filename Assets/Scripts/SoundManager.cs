using UnityEngine;
using System.Collections;
using Popper.Events;

public class SoundManager : MonoBehaviour, ILootActivated
{

    public EventBus _sfxEvents;

    public static SoundManager current;

    public AudioSource[] dotPop;
    public AudioSource LootActivate;
    public AudioSource LootBreak;
    public AudioSource CellOpen;
    public AudioSource dotFail;
    public AudioSource collectorColorChange;


    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public void Construct(EventBus events)
    {
        _sfxEvents = events;

        _sfxEvents.LootActivated += OnLootActivated;
    }

    public void OnLootActivated()
    {
        Debug.Log($"[Sound] (Play) sfx=Lootactivated");
        LootActivate.Play();
    }
}
