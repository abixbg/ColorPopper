using UnityEngine;
using UnityEngine.UI;

namespace Popper.UI.Panels
{
    public class TopPanel : MonoBehaviour
    {
        public UIScorePanel scorePanel;
        public UIClockPanel clockPanlel;
        public AcceptedColorPanel acceptedColorPanel;
        public Button btnReset;

        //services
        private GameManager _gameManager;
        private LevelController _level;
        private DotCollector _collector;

        public void Construct(GameManager gameManager, LevelController level, DotCollector collector)
        {
            _gameManager = gameManager;
            _level = level;
            _collector = collector;

            btnReset.onClick.AddListener(delegate { _gameManager.CmdRestartScene(); });

            acceptedColorPanel.Construct(_level, _collector);
            acceptedColorPanel.SetInitialState();
        }

        private void OnDestroy()
        {
            btnReset.onClick.RemoveAllListeners();
        }
    }
}