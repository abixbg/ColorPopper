using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Popper.UI.Panels
{
    public class TopPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform lootDestination;
        public UIScorePanel scorePanel;
        public UIClockPanel clockPanlel;
        [SerializeField] private AcceptedColorPanel acceptedColorPanel;
        [SerializeField] private Button btnReset;

        public float3 LootCollectionWorldPos => GetCollectorWorldPos(Camera.main);

        public void Construct(GameManager gameManager, LevelController level, ScoreController score)
        {
            btnReset.onClick.AddListener(delegate {gameManager.CmdRestartScene(); });

            acceptedColorPanel.Construct();
            acceptedColorPanel.SetInitialState(level.AcceptedContent.Current.Color);

            scorePanel.Construct(score);
            scorePanel.SetInitialState();

            clockPanlel.Construct(level.Countdown, level.Stopwatch);

        }

        private void OnDestroy()
        {
            btnReset.onClick.RemoveAllListeners();
        }

        private float3 GetCollectorWorldPos(Camera cam)
        {
            Vector3 point = cam.ScreenToWorldPoint(lootDestination.position);
            return point;
        }
    }
}