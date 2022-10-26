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

        public void Init()
        {
            var data = (IPopupWindow)this;

            title.text = data.Title;

            ((RectTransform)gameObject.transform).sizeDelta = data.Style.RectSize;
            background.sprite = data.Style.SpriteBackground;

            if (icon != null)
            {
                icon.sprite = data.Icon;
                icon.color = Color.white;
            }
        }
    }
}
