using UnityEngine;
using System.Collections;

[System.Serializable]
public class Slot : MonoBehaviour
{
    public Dot keyhole { get; set; }
    public Loot loot;

    public int index;

    public SpriteRenderer border;

    public bool isLocked;
    public bool isActive;

    void Awake()
    {
        isActive = true;
    }


    public void CmdClicked()
    {
        if (isActive == true)
        {
            TryKeyhole();            
        }
    }

    void TryKeyhole()
    {    
        if (keyhole.TryUnlock() == true)
        { 
            OpenSlot();     
        }
        else
        {
            BreakSlot();                 
        }
        GameManager.current.currentGrid.ValidateGrid();
    }

    void OpenSlot()
    {

        isActive = false;
        isLocked = false;

        // update slot graphics to unlocked state
        border.enabled = false;

        // activate slot contents
        loot.Activate();

        // disable dot
        keyhole.gameObject.SetActive(false);

    }

    void BreakSlot()
    {
        isActive = false;
        isLocked = true;

        //update graphics to broken state
        border.color = new Color32(140,30,30,255);

        //disable slot contents
        loot.Break();

        // disable dot
        keyhole.gameObject.SetActive(false);
    } 

}
