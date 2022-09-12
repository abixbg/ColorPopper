using UnityEngine;

[System.Serializable]
public struct AudioAssetData
{
    [SerializeField] private string key;
    [SerializeField] private AudioClip clip;

    public AudioAssetData(string key, AudioClip clip)
    {
        this.key = key;
        this.clip = clip;
    }

    public string Key { get => key; }
    public AudioClip Clip { get => clip; }
}
