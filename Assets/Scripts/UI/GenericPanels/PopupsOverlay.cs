using AGK.UI.Overlay;
using AGK.UI.Panels;
using BlockPuzzle;
using System.Threading.Tasks;
using UnityEngine;

namespace AGK.UI.Overlays
{
    public class PopupsOverlay : MonoBehaviour
    {
        [SerializeField] private ScreenSafeArea root;
        [SerializeField] private OverlayBlocker blocker;

        public async Task<GenericPanel> SpawnPopupAsync(GenericPanel panel, int delayMS = 0)
        {
            await Task.Delay(delayMS);
            blocker.SetActive(true);
            var popup = Instantiate(panel, root.transform, false);
            return popup;
        }
    }
}
