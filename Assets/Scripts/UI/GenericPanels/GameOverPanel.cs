using AGK.Core.EventBroadcast;
using AGK.UI.Panels;
using Popper.Events;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : GenericPanel, IPopupWindow, ISingleContentBox
{
    [Header("GameOverPanel")]
    [SerializeField] private MonoBehaviour contentPrefab;
    [SerializeField] private RectTransform contentRoot;
    [SerializeField] private Button btnNew;
    [SerializeField] private Button btnRetry;

    private IEventBus PlayerEvents => GameManager.current.EventsPlayerInput;

    RectTransform ISingleContentBox.Rect => contentRoot;

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

    private void NewLevel()
    {
        PlayerEvents.Broadcast<IPlayerRequestLevel>(s => s.OnLevelLoad());
    }

    private void RetryLevel()
    {
        PlayerEvents.Broadcast<IPlayerRequestLevel>(s => s.OnLevelRetry());
    }

    void IPopupWindow.SetTitle(string title)
    {
        this.title.text = title;
    }

    void IPopupWindow.SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
