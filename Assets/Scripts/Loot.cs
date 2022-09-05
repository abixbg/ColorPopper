using Popper.UI;
using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    public void Activate()
    {
        // activete pop sound
        //int popSoundId = Random.Range(0, SoundManager.current.dotPop.Length - 1);
        //SoundManager.current.dotPop[popSoundId].Play();
        SoundManager.current.LootActivate.Play();

        // move graphic to the collector
        StartCoroutine(MoveTooCollector());

        //switches colorcollector by % chance
        if (Random.value <= 0.3f)
        {
            //Color nextColor = GameManager.current.currentGrid.colorPalette.GetRandomColor();
            GameManager.current.Level.SwitchAcceptedColor();
        }

        //Always Switch Color on Loot activation
        GameManager.current.Level.SwitchAcceptedColor();

        // give points for that 
        GameManager.current.dotScore += 10;
        UIManager.Instance.TopPanel.scorePanel.UpdateValues(GameManager.current.dotScore);

        //add bonus time for collecting correct
        //GameManager.current.remainingCountdown.AddTime(0.5F); //TODO: only Time Buff loot will add time!!!
    }

    // destroys loot
    public void Break()
    {
        gameObject.SetActive(false);
    }

    IEnumerator MoveTooCollector()
    {
        float speed = 15f;
        while (gameObject.transform.position != GameManager.current.LootDestination.position)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, GameManager.current.LootDestination.position, speed * Time.deltaTime);
            yield return null;
        }
    }

}
