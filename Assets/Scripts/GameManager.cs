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
    public Board currentGrid;
    public Countdown remainingCountdown;

    public LevelController Level => levelController;
    public ScoreController Score => scoreController;

    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
        levelController = new LevelController(levelAsset.Data);
        scoreController = new ScoreController();

        currentGrid.Construct(levelController);
        remainingCountdown = new Countdown(levelController.Config.TimeSec);

        levelController.SetPhaseInitialize();
        levelController.StartLevel();

        UIManager.Instance.Init(this);
    }

    private void Update()
    {
        remainingCountdown.OnGameClockUpdate(Time.deltaTime);
    }

    public void CmdRestartScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
