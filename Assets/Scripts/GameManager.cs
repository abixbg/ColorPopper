using Popper.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int defaultLevelCountdownSec;
    public static GameManager current;
    public int currentDifficultyLevel;

    public int dotScore;

    private LevelController levelController;
    public Grid currentGrid;
    public Countdown remainingCountdown;

    public LevelController Level => levelController;

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
        remainingCountdown = new Countdown(defaultLevelCountdownSec);
        //currentDotCollector.Init();
        levelController = new LevelController();
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
