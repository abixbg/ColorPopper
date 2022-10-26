using AGK.UI.Panels;
using UnityEngine;

public class LevelCompletedPanel : GenericPanel, IPopupWindow
{
    [SerializeField] private GenericPanelStyle style;

    string IPopupWindow.Title => "Level Completed";

    GenericPanelStyle IPopupWindow.Style => style;

    Sprite IPopupWindow.Icon
    {
        get
        {
            Debug.LogError("LevelCompletedPanel does not support icons");
            return null;
        }
    }
}
