using AGK.Core.EventBroadcast;
using Popper.Events;
using UnityEngine;
using UnityEngine.UI;

public class AcceptedColorPanel : MonoBehaviour, IAcceptedColorChanged
{
    [SerializeField] private Image colorImage;

    private IEventBus Events => GameManager.current.Events;

    #region Unity MonoBehaviour
    private void OnDestroy()
    {
        Events.Unsubscribe<IAcceptedColorChanged>(this);
    }
    #endregion

    public void Construct()
    {
        Events.Subscribe<IAcceptedColorChanged>(this);
    }

    public void SetInitialState(Color color)
    {
        SetColor(color);
    }

    private void SetColor(Color color)
    {
        colorImage.color = color;
    }

    void IAcceptedColorChanged.OnAcceptedColorChange(SlotContent key)
    {
        SetColor(((ColorSlotKey)key).Color);
    }
}
