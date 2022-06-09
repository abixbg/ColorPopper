using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{

    public void Activate()
    {
        // activete pop sound
        int popSoundId = Random.Range(0, SoundManager.current.dotPop.Length - 1);
        SoundManager.current.dotPop[popSoundId].Play();

        // move graphic to the collector
        StartCoroutine(MoveTooCollector());

        //switches colorcollector by % chance
        if (Random.value <= 0.3f)
        {

            //Color nextColor = GameManager.current.currentGrid.colorPalette.GetRandomColor();
            GameManager.current.currentDotCollector.SwitchAcceptedColor();
        }

        // give points for that 
        GameManager.current.dotScore += 10;
        UIManager.current.scorePanel.UpdateValues();

        //add bonus time for collecting correct
        GameManager.current.remainingCountdown.AddTime(0.5F);
    }

    // destroys loot
    public void Break()
    {
        SoundManager.current.dotFail.Play();
        gameObject.SetActive(false);
    }

    IEnumerator MoveTooCollector()
    {
        float speed = 15f;
        while (gameObject.transform.position != GameManager.current.currentDotCollector.transform.position)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, GameManager.current.currentDotCollector.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }

}
