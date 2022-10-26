using AGK.UI.Overlays;
using AGK.UI.Panels;
using UnityEngine;

namespace Popper
{
    public class PopupFactory : MonoBehaviour
    {
        [SerializeField] private PopupsOverlay overlay;
        [SerializeField] private GameOverPanel gameOverPrefab;
        [SerializeField] private LevelCompletedPanel levelCompletePrefab;

        private void Start()
        {
            SpawnGameOverAsync();
        }

        private void SpawnGameOverAsync()
        {

            var panel = overlay.SpawnPopup(gameOverPrefab);
            ((IPanelContent<PlayerScoreData>)panel).UpdateData(new PlayerScoreData(14, 78));

            var panel2 = overlay.SpawnPopup(levelCompletePrefab);
            ((RectTransform)panel2.gameObject.transform).anchoredPosition = new Vector2(0, -720);
        }
    }
}
