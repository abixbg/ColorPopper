using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int defaultLevelCountdownSec;

    public static GameManager current;
    public int currentDifficultyLevel;
    
    public int dotScore;

    public DotCollector currentDotCollector;
    public Grid currentGrid;
    public Countdown remainingCountdown;
    //public Transform collectorPosition;
    
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
        // currentDotCollector.SetAcceptedColor(Color.blue);
        remainingCountdown = new Countdown(defaultLevelCountdownSec);
        currentDotCollector.Init();
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
