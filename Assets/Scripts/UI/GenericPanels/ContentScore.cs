using AGK.UI.Panels;
using TMPro;
using UnityEngine;

public class ContentScore : MonoBehaviour, IPanelContent<PlayerScoreData>
{
    [SerializeField] private TextMeshProUGUI t1;
    [SerializeField] private TextMeshProUGUI t2;

    RectTransform IPanelContent.Rect => (RectTransform)gameObject.transform;

    void IPanelContent.AttachContent(ISingleContentBox box)
    {
        var rect = ((RectTransform)gameObject.transform);

        rect.SetParent(box.Rect, false);
        rect.sizeDelta = new Vector2(0, 0);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
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
}
