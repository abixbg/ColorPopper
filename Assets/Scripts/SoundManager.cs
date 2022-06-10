using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public static SoundManager current;

    public AudioSource[] dotPop;
    public AudioSource LootActivate;
    public AudioSource LootBreak;
    public AudioSource CellOpen;
    public AudioSource dotFail;
    public AudioSource collectorColorChange;


    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);       
    }
}
