using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScorePanel : MonoBehaviour
{

    public Text dotScoreText;

    void Start()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        //Debug.Log("setscore");
        dotScoreText.text = GameManager.current.dotScore.ToString();       
    }
}
