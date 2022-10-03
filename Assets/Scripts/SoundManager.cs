using UnityEngine;
using System.Collections.Generic;
using Popper.Events;
using AGK.Audio;

public class SoundManager : MonoBehaviour, ILootPicked, ILootConsumed, IAcceptedColorChanged, ISlotStateChanged
{

    public EventBus _sfxEvents;

    public static SoundManager current;

    private IAudioEventPlayer _player;

    [SerializeField] private AudioEventLib eventLibrary;
    [SerializeField] private AudioClipLib clipLibrary;

    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        _player = new AudioPlayerMultiTrack(5, clipLibrary, transform);
    }

    public void Construct(EventBus events)
    {
        _sfxEvents = events;
        _sfxEvents.Subscribe<ILootPicked>(this);
        _sfxEvents.Subscribe<ILootConsumed>(this);
        _sfxEvents.Subscribe<IAcceptedColorChanged>(this);
        _sfxEvents.Subscribe<ISlotStateChanged>(this);
    }

    public void PlaySFX(string key)
    {
        _player.PlaySound(eventLibrary.GetEventData(key), out _);
    }

    void ILootPicked.OnLootPicked()
    {

    }

    void ILootConsumed.OnLootConsumed(Loot _)
    {
        PlaySFX("sfx-star_activated");
    }

    void IAcceptedColorChanged.OnAcceptedColorChange(Color _)
    {
        PlaySFX("sfx-color_change");

        //Vibrate
        //Debug.LogError("Vibrate!");
        //Handheld.Vibrate();
    }

    void ISlotStateChanged.OnSlotOpen(Slot slot)
    {
        //only play 
        if (slot.Loot == null)
            PlaySFX("sfx-cell_open");
    }

    void ISlotStateChanged.OnSlotBreak(Slot slot)
    {
        PlaySFX("sfx-break_slot");
    }
}
