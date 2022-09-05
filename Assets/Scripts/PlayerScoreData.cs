using UnityEngine;

[System.Serializable]
public struct PlayerScoreData
{
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentGame;

    public int Level => currentLevel;
    public int Game => currentGame;

    public PlayerScoreData(int currentLevel, int currentGame)
    {
        this.currentLevel = currentLevel;
        this.currentGame = currentGame;
    }
}
