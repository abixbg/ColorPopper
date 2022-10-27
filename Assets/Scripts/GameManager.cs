using EventBroadcast;
using Popper.Events;
using Popper.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ILevelStateUpdate, IPlayerRequestLevel
{
    [SerializeField] private List<LevelConfigAsset> levelAssets;
    [SerializeField] private int currentLevelIndex;

    public static GameManager current;

    private LevelController levelController;
    private ScoreController scoreController;
    private IEventBus events;
    private IEventBus eventsPlayerInput;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BoardVisual _boardVisual;
    [SerializeField] private GameClock clock;

    public LevelController Level => levelController;
    public IEventBus Events => events;
    public IEventBus EventsPlayerInput => eventsPlayerInput;
    public UIManager UiManager => uiManager;

    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        events = new EventBus();
        eventsPlayerInput = new EventBus();
    }

    void Start()
    {
        uiManager.Construct();
        soundManager.Construct(events);

        levelController = new LevelController(clock, _boardVisual);
        scoreController = new ScoreController();

        events.Subscribe<ILevelStateUpdate>(this);
        eventsPlayerInput.Subscribe<IPlayerRequestLevel>(this);
    }

    public void CmdRestartScene()
    {
        levelController.QuickStartLevel(levelAssets[currentLevelIndex].Data);
    }

    public void CmdEndLevel()
    {
        levelController.RestartLevelAsync();
    }

    void ILevelStateUpdate.OnLevelStartGenerating()
    {
        
    }

    void ILevelStateUpdate.OnLevelCompleted()
    {
        events.Broadcast<ILevelStateUpdate>(s => s.OnLevelFinalScore(scoreController.ScoreData));
    }

    void ILevelStateUpdate.OnLevelFinalScore(PlayerScoreData scoreData)
    {

    }

    void IPlayerRequestLevel.OnLevelLoad()
    {
        if (!levelController.Busy)
            levelController.QuickStartLevel(levelAssets[currentLevelIndex].Data);
    }

    void IPlayerRequestLevel.OnLevelRetry()
    {
        if (!levelController.Busy)
            levelController.RestartLevelAsync();
    }
}
