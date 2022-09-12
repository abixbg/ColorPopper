using UnityEngine;
using System.Collections;
using Popper.Events;

public class SoundManager : MonoBehaviour, ILootActivated
{

    public EventBus _sfxEvents;

    public static SoundManager current;

    private IAudioPlayer _player; 


    public AudioClip lootActivated;

    public AudioSource defaultSource;
    public AudioSource[] dotPop;
    public AudioSource LootBreak;
    public AudioSource CellOpen;
    public AudioSource collectorColorChange;


    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        _player = new AudioPlayer(defaultSource);
    }

    public void Construct(EventBus events)
    {
        _sfxEvents = events;

        _sfxEvents.LootActivated += OnLootActivated;
    }

    public void OnLootActivated()
    {

        var data = new AudioAssetData("sfx_star_activated", lootActivated);
        _player.PlaySound(data);
    }
}
