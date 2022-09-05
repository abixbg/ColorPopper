using Popper.UI.Panels;
using UnityEngine;

namespace Popper.UI
{
    //TODO: Rename to OverlayUIManager
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform safeArea;
        [SerializeField] private TopPanel topPanelPrefab;

        private static UIManager _instance;
        private TopPanel _topPanel;

        public static UIManager Instance => _instance;

        public TopPanel TopPanel => _topPanel;

        void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(gameObject);
        }

        public void Init(GameManager gameManager)
        {
            _topPanel = Instantiate(topPanelPrefab, safeArea, false);
            _topPanel.Construct(gameManager, gameManager.Level, gameManager.currentDotCollector);
        }
    }
}