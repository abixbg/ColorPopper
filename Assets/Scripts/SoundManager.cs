using AGK.Audio;
using AGK.Core.EventBroadcast;
using Popper.Events;
using UnityEngine;

public class SoundManager :
    MonoBehaviour,
    ILootConsumed,
    IAcceptedColorChanged,
    ISlotVisualStateChanged
{
    public IEventBus _sfxEvents;
    private IAudioEventPlayer _player;

    [SerializeField] private AudioEventLib eventLibrary;
    [SerializeField] private AudioClipLib clipLibrary;


    void Awake()
    {
        _player = new AudioPlayerMultiTrack(5, clipLibrary, transform);
    }

    public void Construct(IEventBus events)
    {
        _sfxEvents = events;
        _sfxEvents.Subscribe<ILootConsumed>(this);
        _sfxEvents.Subscribe<IAcceptedColorChanged>(this);
        _sfxEvents.Subscribe<ISlotVisualStateChanged>(this);
    }

    public void PlaySFX(string key)
    {
        var eventKey = new AudioEventKey(key);

        _player.PlaySound(eventLibrary.GetResource(eventKey), out _);
    }

    void ILootConsumed.OnLootConsumed(SlotLoot _)
    {
        PlaySFX("sfx-star_activated");
    }

    void IAcceptedColorChanged.OnAcceptedColorChange(SlotContent _)
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
