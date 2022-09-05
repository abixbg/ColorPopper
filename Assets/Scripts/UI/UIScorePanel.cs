using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScorePanel : MonoBehaviour
{

    public Text dotScoreText;

    void Start()
    {
        UpdateValues(0);
    }

    public void UpdateValues(int score)
    {
        dotScoreText.text = score.ToString();
    }
}
