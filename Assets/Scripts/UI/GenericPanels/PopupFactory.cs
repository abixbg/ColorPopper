using AGK.Core.EventBroadcast;
using AGK.UI.Overlays;
using AGK.UI.Panels;
using Popper.Events;
using UnityEngine;

namespace Popper
{
    public class PopupFactory : MonoBehaviour, ILevelStateUpdate
    {
        [SerializeField] private PopupsOverlay overlay;
        [SerializeField] private GenericPanel gameOverPrefab;
        [SerializeField] private GenericPanel levelCompletePrefab;

        [SerializeField] private MonoBehaviour scoreContentPrefab;


        public IEventBus Events => GameManager.current.Events;

        private void Start()
        {
            Events.Subscribe<ILevelStateUpdate>(this);
        }

        void ILevelStateUpdate.OnLevelStartGenerating()
        {
            overlay.CloseAll();
        }

        void ILevelStateUpdate.OnLevelCompleted()
        {

        }

        async void ILevelStateUpdate.OnLevelFinalScore(PlayerScoreData scoreData)
        {
            //Validation TODO: Move to OnValidate() or implement proper content lib;
            if (!(scoreContentPrefab is IPanelContent<PlayerScoreData>))
            {
                Debug.LogAssertion($"[scoreContentPrefab] does not implement [ IPanelContent<PlayerScoreData> ]");
                return;
            }

            var panel = await overlay.SpawnPopupAsync(gameOverPrefab);
            var window = (IPopupWindow)panel;
            var contentBox = (ISingleContentBox)panel;
            var scoreContent = Instantiate(scoreContentPrefab) as IPanelContent<PlayerScoreData>;

            window.SetTitle("Game Over");
            window.SetIcon(panel.Style.SpriteIcon);
            window.SetStyle(panel.Style);

            scoreContent.UpdateData(scoreData);
            scoreContent.AttachContent(contentBox);
        }
    }
}
