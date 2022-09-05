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

        //services
        private GameManager _gameManager;
        private LevelController _level;

        public void Construct(GameManager gameManager, LevelController level)
        {
            _gameManager = gameManager;
            _level = level;

            btnReset.onClick.AddListener(delegate { _gameManager.CmdRestartScene(); });

            acceptedColorPanel.Construct(_level);
            acceptedColorPanel.SetInitialState();

            clockPanlel.Construct(level.Countdown);
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