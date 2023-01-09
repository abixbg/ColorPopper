using AGK.Core.EventBroadcast;
using Popper.Events;
using UnityEngine;
using UnityEngine.UI;

public class UIScorePanel : MonoBehaviour, IPlayerScoreChanged
{
    public Text dotScoreText;

    private IEventBus Events => GameManager.current.Events;

    private void OnDestroy()
    {
        Events.Unsubscribe<IPlayerScoreChanged>(this);
    }
    public void Construct()
    {
        Events.Subscribe<IPlayerScoreChanged>(this);
    }

    private void UpdateValues(int score)
    {
        dotScoreText.text = score.ToString();
    }

    void IPlayerScoreChanged.OnUpdatePlayerScoreData(PlayerScoreData data)
    {
        UpdateValues(data.LevelPoints);
    }
}
