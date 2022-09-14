using UnityEngine;
using System.Collections.Generic;
using Popper.Events;
using AGK.Audio;

public class SoundManager : MonoBehaviour, ILootPicked, ILootConsumed
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
    }

    public void OnLootPicked()
    {
        PlaySFX("sfx-star_activated");
    }

    public void OnLootConsumed()
    {
        PlaySFX("sfx-star_activated");
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
}
