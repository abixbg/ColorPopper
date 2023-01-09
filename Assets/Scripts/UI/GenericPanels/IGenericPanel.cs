using AGK.UI.Panels;
using UnityEngine;

public interface IPopupWindow
{
    void SetTitle(string title);
    void SetIcon(Sprite icon);
    void SetStyle(GenericPanelStyle Style);
}
