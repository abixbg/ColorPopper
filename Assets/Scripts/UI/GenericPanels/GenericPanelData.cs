using UnityEngine;

public  struct GenericPanelData
{
    [SerializeField] private readonly string title;

    public string Title => title;

    public GenericPanelData(string title)
    {
        this.title = title;
    }
}
