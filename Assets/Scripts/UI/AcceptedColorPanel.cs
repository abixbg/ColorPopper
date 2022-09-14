using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popper.Events;

public class AcceptedColorPanel : MonoBehaviour, IAcceptedColorChanged
{
    [SerializeField] private Image colorImage;

    #region Unity MonoBehaviour
    private void OnDestroy()
    {
        GameManager.current.Events.Unsubscribe<IAcceptedColorChanged>(this);
    }
    #endregion

    public void Construct()
    {
        GameManager.current.Events.Subscribe<IAcceptedColorChanged>(this);
    }

    public void SetInitialState(Color color)
    {
        SetColor(color);
    }

    private void SetColor(Color color)
    {
        colorImage.color = color;
    }

    void IAcceptedColorChanged.OnAcceptedColorChange(Color color)
    {
        SetColor(color);
    }
}
