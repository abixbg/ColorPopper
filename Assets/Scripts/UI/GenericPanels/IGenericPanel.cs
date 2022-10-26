using AGK.UI.Panels;
using UnityEngine;

public interface IPopupWindow
{
    string Title { get; }
    Sprite Icon { get; }
    GenericPanelStyle Style { get; }
}
