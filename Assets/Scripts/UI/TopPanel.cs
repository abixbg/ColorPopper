using AGK.Core.EventBroadcast;
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
        [SerializeField] private Button btnTEST;

        public float3 LootCollectionWorldPos => GetCollectorWorldPos(Camera.main);
        private IEventBus PlayerInput => GameManager.current.EventsPlayerInput;

        public void Construct()
        {
            btnReset.onClick.AddListener(PlayerRequestStartLevel);
            btnTEST.onClick.AddListener(PlayerRequestRetryLevel);

            acceptedColorPanel.Construct();
            scorePanel.Construct();
            clockPanlel.Construct();
        }

        private void OnDestroy()
        {
            btnReset.onClick.RemoveAllListeners();
            btnTEST.onClick.RemoveAllListeners();
        }

        private float3 GetCollectorWorldPos(Camera cam)
        {
            Vector3 point = cam.ScreenToWorldPoint(lootDestination.position);
            return point;
        }

        private void PlayerRequestStartLevel()
        {
            PlayerInput.Broadcast<IPlayerRequestLevel>(s => s.OnLevelLoad(), true);
        }

        private void PlayerRequestRetryLevel()
        {
            PlayerInput.Broadcast<IPlayerRequestLevel>(s => s.OnLevelRetry(), true);
        }
    }
}