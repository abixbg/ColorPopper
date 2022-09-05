using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Popper.UI.Panels;


//TODO: Rename to OverlayUIManager
public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TopPanel topPanelPrefab;
    [SerializeField] private RectTransform safeArea;


    public AcceptedColorPanel acceptedColorPanel;

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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _topPanel = Instantiate(topPanelPrefab, safeArea, false);
    }
}
