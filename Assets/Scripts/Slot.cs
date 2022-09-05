using UnityEngine;
using System.Collections;

[System.Serializable]
public class Slot : MonoBehaviour
{
    public Dot keyhole { get; set; }
    
    [SerializeField] private Loot loot;
    public Loot Loot { get => loot; set => loot = value; }

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
        if (loot != null)
        {
            loot.Activate();
        }
        else
        {
            SoundManager.current.CellOpen.Play();
        }
            

        // disable dot
        keyhole.gameObject.SetActive(false);

        //switches colorcollector by % chance
        if (Random.value <= 0.3f)
        {
            GameManager.current.Level.SwitchAcceptedColor();
        }
    }

    void BreakSlot()
    {
        isActive = false;
        isLocked = true;

        //update graphics to broken state
        border.color = new Color32(140,30,30,255);

        //disable slot contents
        if (loot != null)
        {
            loot.Break();
        }

        SoundManager.current.LootBreak.Play();

        // disable dot
        keyhole.gameObject.SetActive(false);
    } 

}
