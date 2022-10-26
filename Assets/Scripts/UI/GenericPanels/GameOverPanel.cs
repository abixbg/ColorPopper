using AGK.UI.Panels;
using TMPro;
using UnityEngine;

public class GameOverPanel : GenericPanel, IPopupWindow, IPanelContent<PlayerScoreData>
{
    [Header("GameOverPanel")]
    [SerializeField] private TextMeshProUGUI t1;
    [SerializeField] private TextMeshProUGUI t2;
    [SerializeField] private GenericPanelStyle style;

    string IPopupWindow.Title => "GameOver";

    GenericPanelStyle IPopupWindow.Style => style;
    Sprite IPopupWindow.Icon => style.SpriteIcon;

    void IPanelContent<PlayerScoreData>.SetInitialState()
    {
        t1.text = "{time}";
        t1.text = "{time2}";
    }

    void IPanelContent<PlayerScoreData>.UpdateData(PlayerScoreData data)
    {
        t1.text = data.LevelPoints.ToString();
        t2.text = data.Game.ToString();
    }
}
