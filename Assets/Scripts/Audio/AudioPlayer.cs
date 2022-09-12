using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : IAudioPlayer
{
    public AudioSource defaultSource;

    public AudioPlayer(AudioSource source)
    {
        this.defaultSource = source;
    }

    public AudioPlaybackHandle PlaySound(AudioAssetData data)
    {
        Debug.Log($"[Sound] (Play) sfx={data.Key}");
        defaultSource.clip = data.Clip;
        defaultSource.Play();

        var handle = new AudioPlaybackHandle(defaultSource);
        return handle;
    }
}
