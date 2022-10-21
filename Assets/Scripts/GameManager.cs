using Popper.Events;
using Popper.UI;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, ILevelStateUpdate
{
    [SerializeField] private List<LevelConfigAsset> levelAssets;
    [SerializeField] private int currentLevelIndex;

    public static GameManager current;

    private LevelController levelController;
    private ScoreController scoreController;
    private EventBus events;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BoardVisual _boardVisual;
    [SerializeField] private GameClock clock;

    public LevelController Level => levelController;
    public EventBus Events => events;
    public UIManager UiManager => uiManager;

    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        events = new EventBus();
    }

    void Start()
    {
        uiManager.Construct(this, levelController, scoreController);
        soundManager.Construct(events);

        levelController = new LevelController(clock, _boardVisual);
        scoreController = new ScoreController();

        levelController.QuickStartLevel(levelAssets[currentLevelIndex].Data);

        events.Subscribe<ILevelStateUpdate>(this);
    }

    public void CmdRestartScene()
    {
        levelController.QuickStartLevel(levelAssets[currentLevelIndex].Data);
    }

    public void CmdEndLevel()
    {
        levelController.RestartLevel();
    }

    void ILevelStateUpdate.OnLevelCompleted()
    {
        levelController.RestartLevel();
    }
}
