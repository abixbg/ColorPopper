using UnityEngine;
using UnityEngine.UI;

namespace Popper.UI.Panels
{
    public class TopPanel : MonoBehaviour
    {
        public UIScorePanel scorePanel;
        public UIClockPanel clockPanlel;
        public Button btnReset;

        private void Start()
        {
            btnReset.onClick.AddListener(delegate { GameManager.current.CmdRestartScene(); });
        }

        private void OnDestroy()
        {
            btnReset.onClick.RemoveAllListeners();
        }
    }
}