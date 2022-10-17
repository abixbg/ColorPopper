﻿using AGK.Audio;
using Popper.Events;
using UnityEngine;

public class SoundManager :
    MonoBehaviour,
    ILootConsumed,
    IAcceptedColorChanged,
    ISlotVisualStateChanged
{
    public EventBus _sfxEvents;
    private IAudioEventPlayer _player;

    [SerializeField] private AudioEventLib eventLibrary;
    [SerializeField] private AudioClipLib clipLibrary;


    void Awake()
    {
        _player = new AudioPlayerMultiTrack(5, clipLibrary, transform);
    }

    public void Construct(EventBus events)
    {
        _sfxEvents = events;
        _sfxEvents.Subscribe<ILootConsumed>(this);
        _sfxEvents.Subscribe<IAcceptedColorChanged>(this);
        _sfxEvents.Subscribe<ISlotVisualStateChanged>(this);
    }

    public void PlaySFX(string key)
    {
        _player.PlaySound(eventLibrary.GetEventData(key), out _);
    }

    void ILootConsumed.OnLootConsumed(SlotLoot _)
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

    void ISlotVisualStateChanged.OnOpenSuccess(SlotData slot)
    {
        PlaySFX("sfx-cell_open");
    }

    void ISlotVisualStateChanged.OnBreak(SlotData slot)
    {
        PlaySFX("sfx-break_slot");
    }
}
