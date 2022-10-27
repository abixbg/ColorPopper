using AGK.UI.Overlays;
using AGK.UI.Panels;
using EventBroadcast;
using Popper.Events;
using UnityEngine;

namespace Popper
{
    public class PopupFactory : MonoBehaviour, ILevelStateUpdate
    {
        [SerializeField] private PopupsOverlay overlay;
        [SerializeField] private GenericPanel gameOverPrefab;
        [SerializeField] private GenericPanel levelCompletePrefab;

        public IEventBus Events => GameManager.current.Events;

        private void Start()
        {
            Events.Subscribe<ILevelStateUpdate>(this);
        }

        private async void SpawnGameOverAsync()
        {
            var panel = await overlay.SpawnPopupAsync(gameOverPrefab, 2000);
            ((IPopupWindow)panel).SetTitle("Game Over", panel.Style.SpriteIcon);
            ((IPopupWindow)panel).SetStyle(panel.Style);
            ((IPanelContent<PlayerScoreData>)panel).UpdateData(new PlayerScoreData(14, 78));
        }

        void ILevelStateUpdate.OnLevelCompleted()
        {

        }

        async void ILevelStateUpdate.OnLevelFinalScore(PlayerScoreData scoreData)
        {
            var panel = await overlay.SpawnPopupAsync(gameOverPrefab, 2000);
            ((IPopupWindow)panel).SetTitle("Game Over", panel.Style.SpriteIcon);
            ((IPopupWindow)panel).SetStyle(panel.Style);
            ((IPanelContent<PlayerScoreData>)panel).UpdateData(scoreData);
        }
    }
}
