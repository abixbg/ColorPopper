using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptedColorPanel : MonoBehaviour
{
    [SerializeField] private Image colorImage; 
    private DotCollector _collector;
    private LevelController _level;

    private DotCollector Collector => _collector;
    private LevelController Level => _level;

    #region Unity MonoBehaviour
    private void OnDestroy()
    {
        Level.AcceptedColorChanged -= OnAcceptedColorChanged;
    }
    #endregion

    public void Construct(LevelController level, DotCollector collector)
    {
        _level = level;
        _collector = collector;

        _level.AcceptedColorChanged += OnAcceptedColorChanged;
    }

    public void SetInitialState()
    {
        SetColor(Level.AcceptedColor);
    }

    private void OnAcceptedColorChanged()
    {
        SetColor(Level.AcceptedColor);
    }

    private void SetColor(Color color)
    {
        colorImage.color = color;
    }
}
