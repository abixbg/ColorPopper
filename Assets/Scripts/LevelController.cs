using AGK.GameGrids;
using EventBroadcast;
using Popper.Events;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelController :
    ISlotClicked,
    ISlotStateChanged,
    ILootPicked,
    ILootConsumed
{
    private LevelConfigData _levelData;
    private Color _acceptedColor;
    private readonly Countdown _countdown;
    private readonly Stopwatch _stopwatch;
    public readonly BubblePoolColors _keyPool;

    public Color AcceptedColor { get => _acceptedColor; }
    public LevelConfigData Config => _levelData;
    public Countdown Countdown => _countdown;
    public Stopwatch Stopwatch => _stopwatch;   
    public BubblePoolColors KeyPool => _keyPool;

    public float TimeRemaining => _countdown.TimeRemaining;

    public int BoardCellremaining => _board.RemainingSlots;

    private IEventBus _events;
    private BoardVisual _board;

    public LevelController(LevelConfigData levelData, EventBus levelEvents, GameClock clock)
    {
        _levelData = levelData;
        _events = levelEvents;
        _countdown = new Countdown(levelData.TimeSec);
        _stopwatch = new Stopwatch(clock);
        _stopwatch.SetActive(true);
        _keyPool = new BubblePoolColors(levelData.Pallete);

        _events.Subscribe<ILootPicked>(this);
        _events.Subscribe<ILootConsumed>(this);
        _events.Subscribe<ISlotClicked>(this);
        _events.Subscribe<ISlotStateChanged>(this);
    }

    public void SetPhaseInitialize(BoardVisual board)
    {
        _board = board;
        _board.OnLevelPhaseInitialize();
        Debug.Log("[Level] Initialilze!");
    }

    public void StartLevel()
    {
        Debug.Log("[Level] Start!");
        SwitchAcceptedColor();
    }

    private void SwitchAcceptedColor()
    {
        var current = _acceptedColor;

        //get random color from the remaining in the grid
        _acceptedColor = _keyPool.GetRandom().Color;

        //Make sure it's really changed
        while (_acceptedColor == current && GameManager.current.Level.KeyPool.Remaining > 1)
            _acceptedColor = _keyPool.GetRandom().Color;

        //only rise event when the 
        _events.Broadcast<IAcceptedColorChanged>(s => s.OnAcceptedColorChange(_acceptedColor));
    }

    void ISlotClicked.OnSlotClicked(Slot slot)
    {
        var key = new ColorSlotKey(_acceptedColor);

        if (slot.Keyhole.IsMatch(key))       
            slot.OpenSlot();        
        else
            slot.BreakSlot();
    }

    void ISlotStateChanged.OnSlotOpen(Slot slot)
    {
        var key = new ColorSlotKey(slot.Keyhole.Color);

        if (!GameManager.current.Board.HaveKeyHoleOnBoard(key))
        {
            GameManager.current.Level.KeyPool.Remove(key);
        }
        
        bool validBoard = ValidBoard();
        bool randomSwitch = UnityEngine.Random.value <= 0.05f;
        bool hasLoot = slot.Loot != null;

        if (BoardCellremaining <1)
        {
            Debug.LogAssertion($"LevelCompleted!");
            _stopwatch.SetActive(false);
            return;
        }

        if (!validBoard || randomSwitch || hasLoot)
            SwitchAcceptedColor();
    }

    void ISlotStateChanged.OnSlotBreak(Slot slot)
    {
        var key = new ColorSlotKey(slot.Keyhole.Color);

        if (!GameManager.current.Board.HaveKeyHoleOnBoard(key))
        {
            GameManager.current.Level.KeyPool.Remove(key);
        }

        if (BoardCellremaining < 1)
        {
            Debug.LogAssertion($"LevelCompleted!");
            _stopwatch.SetActive(false);
            return;
        }

        SwitchAcceptedColor();
    }

    private bool ValidBoard()
    {
        bool valid = GameManager.current.Board.HaveKeyHoleOnBoard(new ColorSlotKey(_acceptedColor));
        return valid;
    }

    void ILootPicked.OnLootPicked(Loot loot)
    {
        foreach (var slot in loot.ConnectedSlots)
        {
            slot.Loot = null;
            slot.OpenSlot();
        }
    }

    void ILootConsumed.OnLootConsumed(Loot loot)
    {
        Debug.Log("[Level] AddTime!");
        _countdown.AddTime(3);
    }
}
