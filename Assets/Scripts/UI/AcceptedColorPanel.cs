using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptedColorPanel : MonoBehaviour
{
    [SerializeField] private Image colorImage; 
    [SerializeField] private DotCollector collector;


    private void Start()
    {
        collector = GameManager.current.currentDotCollector;
        collector.AcceptedColorChanged += OnAcceptedColorChanged;
        SetColor(collector.AcceptedColor);
    }

    private void OnDisable()
    {        
        collector.AcceptedColorChanged -= OnAcceptedColorChanged;
    }

    private void OnAcceptedColorChanged()
    {
        SetColor(collector.AcceptedColor);
    }

    private void SetColor(Color color)
    {
        colorImage.color = color;
    }
}
