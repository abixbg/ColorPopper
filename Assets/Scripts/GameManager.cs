using Popper.Events;
using Popper.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        levelController = new LevelController(levelAsset.Data, clock, _boardVisual);
        scoreController = new ScoreController();

        levelController.QuickStartLevel();
    }

    public void CmdRestartScene()
    {
        levelController.QuickStartLevel();
    }

    public void CmdEndLevel()
    {
        levelController.RestartLevel();
    }
}
