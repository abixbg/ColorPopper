using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScorePanel : MonoBehaviour
{

    public Text dotScoreText;

    private ScoreController scoreController;

    private void OnDestroy()
    {
        scoreController.ScoreChanged -= OnScoreUpdate;
    }
    public void Construct(ScoreController scoreController)
    {
        this.scoreController = scoreController;

        scoreController.ScoreChanged += OnScoreUpdate;
    }

    public void SetInitialState()
    {
        UpdateValues(0);
    }

    private void OnScoreUpdate()
    {
        UpdateValues(scoreController.ScoreData.Level);
    }

    private void UpdateValues(int score)
    {
        dotScoreText.text = score.ToString();
    }
}
