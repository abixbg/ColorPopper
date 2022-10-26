using AGK.UI.Overlay;
using AGK.UI.Panels;
using BlockPuzzle;
using UnityEngine;

namespace AGK.UI.Overlays
{
    public class PopupsOverlay : MonoBehaviour
    {
        [SerializeField] private ScreenSafeArea root;
        [SerializeField] private OverlayBlocker blocker;

        public GenericPanel SpawnPopup(GenericPanel panel)
        {
            blocker.SetActive(true);
            var popup = Instantiate(panel, root.transform, false);
            panel.Init();

            return popup;
        }
    }
}
