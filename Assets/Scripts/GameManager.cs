using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Popper.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int defaultLevelCountdownSec;
    [SerializeField] private Transform lootDestination; //Where loot icons will go and be destroyed

    public static GameManager current;
    public int currentDifficultyLevel;
    
    public int dotScore;

    public DotCollector currentDotCollector;
    private LevelController levelController;
    public Grid currentGrid;
    public Countdown remainingCountdown;
    //public Transform collectorPosition;

    public LevelController Level => levelController;
    public Transform LootDestination => lootDestination;


    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        remainingCountdown = new Countdown(defaultLevelCountdownSec);
        //currentDotCollector.Init();
        levelController = new LevelController(currentDotCollector);
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
