using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlaybackHandle
{
    private AudioSource source;

    public AudioPlaybackHandle(AudioSource source)
    {
        this.source = source;
    }

    public AudioSource Source { get => source; }
}
