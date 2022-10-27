using AGK.UI.Panels;
using EventBroadcast;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : GenericPanel, IPopupWindow, IPanelContent<PlayerScoreData>
{
    [Header("GameOverPanel")]
    [SerializeField] private TextMeshProUGUI t1;
    [SerializeField] private TextMeshProUGUI t2;
    [SerializeField] private Button btnNew;
    [SerializeField] private Button btnRetry;

    private IEventBus PlayerEvents => GameManager.current.EventsPlayerInput; 

    private void Start()
    {
        btnNew.onClick.AddListener(NewLevel);
        btnRetry.onClick.AddListener(RetryLevel);
    }

    void IPopupWindow.SetStyle(GenericPanelStyle style)
    {
        ((RectTransform)gameObject.transform).sizeDelta = style.RectSize;
        background.sprite = style.SpriteBackground;
    }

    void IPopupWindow.SetTitle(string title, Sprite icon)
    {
        this.title.text = title;
        this.icon.sprite = icon;
    }

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

    private void NewLevel()
    {
        PlayerEvents.Broadcast<IPlayerRequestLevel>(s => s.OnLevelLoad());
    }

    private void RetryLevel()
    {
        PlayerEvents.Broadcast<IPlayerRequestLevel>(s => s.OnLevelRetry());
    }
}
