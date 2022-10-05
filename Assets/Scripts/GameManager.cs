using Popper.UI;
using Popper.Events;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using AGK.GameGrids;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfigAsset levelAsset;

    public static GameManager current;

    private LevelController levelController;
    private ScoreController scoreController;
    private EventBus events;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BoardVisual _boardVisual;
    [SerializeField] private GameClock clock;

    public BoardVisual Board => _boardVisual;
    public LevelController Level => levelController;
    public EventBus Events => events;
    public UIManager UiManager => uiManager;

    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        events = new EventBus();
    }

    void Start()
    {
        levelController = new LevelController(levelAsset.Data, events, clock);
        scoreController = new ScoreController(events);

        soundManager.Construct(events);
        uiManager.Construct(this, levelController, scoreController);

        Debug.Log("[Level] Initialilze!");

        _boardVisual.Construct(levelController.Grid, levelController);
        _boardVisual.SpawnCells();
        _boardVisual.OnLevelPhaseInitialize();
        levelController.StartLevel();
    }

    private void Update()
    {
        levelController.Countdown.ConsumeTime(Time.deltaTime);
    }

    public void CmdRestartScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
