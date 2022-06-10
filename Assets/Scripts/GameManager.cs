using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{

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

        currentDotCollector.Init();
    }

    public void CmdRestartScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
