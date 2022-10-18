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

    async void Start()
    {
        levelController = new LevelController(levelAsset.Data, events, clock, _boardVisual);
        scoreController = new ScoreController(events);

        soundManager.Construct(events);
        uiManager.Construct(this, levelController, scoreController);

        levelController.QuickStartLevel();
    }

    private void Update()
    {
        levelController.Countdown.ConsumeTime(Time.deltaTime);
    }

    public void CmdRestartScene()
    {
        //SceneManager.LoadScene(0, LoadSceneMode.Single);
        levelController.QuickStartLevel();
    }

    public void CmdEndLevel()
    {
        levelController.RestartLevel();
    }
}
