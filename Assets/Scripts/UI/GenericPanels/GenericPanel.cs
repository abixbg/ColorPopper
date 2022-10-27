using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AGK.UI.Panels
{
    public class GenericPanel : MonoBehaviour
    {
        [SerializeField] protected Image background;
        [SerializeField] protected Image icon;
        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected GenericPanelStyle style;

        public GenericPanelStyle Style => style;

        public RectTransform RectTransform => (RectTransform)gameObject.transform;
    }
}
