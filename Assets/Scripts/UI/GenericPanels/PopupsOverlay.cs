using AGK.UI.Overlay;
using AGK.UI.Panels;
using BlockPuzzle;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AGK.UI.Overlays
{
    public class PopupsOverlay : MonoBehaviour
    {
        [SerializeField] private ScreenSafeArea root;
        [SerializeField] private OverlayBlocker blocker;

        private List<GenericPanel> openPanels;

        private void Awake()
        {
            openPanels = new List<GenericPanel>();
        }

        public async Task<GenericPanel> SpawnPopupAsync(GenericPanel prefab, int delayMS = 0)
        {
            await Task.Delay(delayMS);
            blocker.SetActive(true);
            var panel = Instantiate(prefab, root.transform, false);

            openPanels.Add(panel);

            return panel;
        }

        public void CloseAll()
        {
            foreach (var panel in openPanels)
            {
                panel.Close();
            }

            openPanels.Clear();
            blocker.SetActive(false);
        }
    }
}
