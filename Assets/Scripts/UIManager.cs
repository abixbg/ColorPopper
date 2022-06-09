using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager current;

    //Ui Dialogs
    //public UIGameOverDialog dlgGameOver;
    //public UIHelpDialog dlgHelp;
    ////public UIHelpDialog pauseDialog;
    //public UIPlayerInput playerUiInput;
    public UIScorePanel scorePanel;
    public UIClockPanel clockPanlel;


    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

}
