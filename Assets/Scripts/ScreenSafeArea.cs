using UnityEngine;

namespace BlockPuzzle
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenSafeArea : MonoBehaviour
    {
        [SerializeField] private bool emulate;
        [SerializeField] private Rect emulatedSafeArea;

        private RectTransform _rect;
        void Awake()
        {
            _rect = (RectTransform)transform;
        }

        private void Start()
        {
            Apply();
        }

        private void Apply()
        {
            Rect safeRect = GetSafeRect();
            Vector2 anchorMin = safeRect.position;
            Vector2 anchorMax = safeRect.position + safeRect.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rect.anchorMin = anchorMin;
            _rect.anchorMax = anchorMax;
            _rect.sizeDelta = Vector2.zero;
        }

        private Rect GetSafeRect()
        {
            if (!emulate)
                return Screen.safeArea;
            else
                return emulatedSafeArea;
        }
    }
}
