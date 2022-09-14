using UnityEngine;
using System.Collections.Generic;
using Popper.Events;
using AGK.Audio;

public class SoundManager : MonoBehaviour, ILootPicked, ILootConsumed, IAcceptedColorChanged, ISlotStateChanged
{

    public EventBus _sfxEvents;

    public static SoundManager current;

    private IAudioPlayer _player; 

    [SerializeField] private List<AudioAssetData> clipLibrary;

    public AudioSource defaultSource;

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
        _sfxEvents.Subscribe<ILootPicked>(this);
        _sfxEvents.Subscribe<ILootConsumed>(this);
        _sfxEvents.Subscribe<IAcceptedColorChanged>(this);
        _sfxEvents.Subscribe<ISlotStateChanged>(this);
    }

    public void PlaySFX(string key)
    {
        var data = new AudioAssetData(key, GetClip(key));
        _player.PlaySound(data);
    }

    public AudioClip GetClip(string key)
    {
        var audioAsset  = clipLibrary.Find(cl => string.Equals(key, cl.Key));
        if (audioAsset.Clip == null)
            Debug.LogAssertion($"Not Found: {key}");
        return audioAsset.Clip;
    }

    void ILootPicked.OnLootPicked()
    {
        PlaySFX("sfx-star_activated");
    }

    void ILootConsumed.OnLootConsumed()
    {
        PlaySFX("sfx-star_activated");
    }

    void IAcceptedColorChanged.OnAcceptedColorChange(Color _)
    {
        PlaySFX("sfx-color_change");
    }

    void ISlotStateChanged.OnSlotOpen(Slot slot)
    {
        PlaySFX("sfx-cell_open");
    }

    void ISlotStateChanged.OnSlotBreak(Slot slot)
    {
        PlaySFX("sfx-break_slot");
    }
}
