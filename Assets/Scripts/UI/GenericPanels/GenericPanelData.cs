using UnityEngine;

public readonly struct GenericPanelData
{
    [SerializeField] private readonly string title;

    public string Title => title;

    public GenericPanelData(string title)
    {
        this.title = title;
    }
}
