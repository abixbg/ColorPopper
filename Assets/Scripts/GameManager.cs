using Popper.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelConfigAsset levelAsset;

    public static GameManager current;

    private LevelController levelController;
    private ScoreController scoreController;
    private EventBus events;

    public Board currentGrid;

    public LevelController Level => levelController;

    public ScoreController Score => scoreController;
    public EventBus Events => events;

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
        levelController = new LevelController(levelAsset.Data, events);
        scoreController = new ScoreController(events);

        currentGrid.Construct(levelController);

        levelController.SetPhaseInitialize();
        levelController.StartLevel();

        UIManager.Instance.Init(this);
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
