using UnityEngine;
using UnityEngine.UI;

namespace AGK.UI.Overlay
{
    [RequireComponent(typeof(Image))]
    public class OverlayBlocker : MonoBehaviour
    {
        [SerializeField] private Image background;

        private void Awake()
        {
            background = GetComponent<Image>();
            background.enabled = false;
        }

        public void SetActive(bool state)
        {
            background.enabled = state;
        }
    }
}
