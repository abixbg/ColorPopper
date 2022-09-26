using EventBroadcast;
using Popper.Events;
using UnityEngine;

public class LevelController :
    ISlotClicked,
    ISlotStateChanged,
    ILootConsumed
{
    private LevelConfigData _levelData;
    private Color _acceptedColor;
    private readonly Countdown _countdown;
    private readonly Stopwatch _stopwatch;

    public Color AcceptedColor { get => _acceptedColor; }
    public LevelConfigData Config => _levelData;
    public Countdown Countdown => _countdown;
    public Stopwatch Stopwatch => _stopwatch;

    public float TimeRemaining => _countdown.TimeRemaining;

    private IEventBus _events;
    private BoardVisual _board;

    public LevelController(LevelConfigData levelData, EventBus levelEvents, GameClock clock)
    {
        _levelData = levelData;
        _events = levelEvents;
        _countdown = new Countdown(levelData.TimeSec);
        _stopwatch = new Stopwatch(clock);

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
        _acceptedColor = GameManager.current.Board.GetRandomColor();

        //Make sure it's really changed
        while (_acceptedColor == current && GameManager.current.Board.RemainingColors > 1)
            _acceptedColor = GameManager.current.Board.GetRandomColor();

        //only rise event when the 
        _events.Broadcast<IAcceptedColorChanged>(s => s.OnAcceptedColorChange(_acceptedColor));
    }

    public bool IsAcceptableColor(Color col)
    {
        bool foo = false;
        if (col == _acceptedColor) foo = true;
        return foo;
    }

    void ISlotClicked.OnSlotClicked(Slot slot)
    {
        if (Accepted(slot))
            slot.OpenSlot();
        else
            slot.BreakSlot();
    }

    public bool Accepted(Slot slot)
    {
        if (GameManager.current.Level.IsAcceptableColor(slot.Keyhole.Color) == true)
        {
            return true;
        }
        else return false;
    }

    void ISlotStateChanged.OnSlotOpen(Slot slot)
    {
        GameManager.current.Board.UpdateColorList();

        bool validBoard = ValidBoard();
        bool randomSwitch = UnityEngine.Random.value <= 0.05f;
        bool hasLoot = slot.Loot != null;

        if (!validBoard || randomSwitch || hasLoot)
            SwitchAcceptedColor();
    }

    void ISlotStateChanged.OnSlotBreak(Slot slot)
    {
        GameManager.current.Board.UpdateColorList();
        SwitchAcceptedColor();
    }

    private bool ValidBoard()
    {
        bool valid = GameManager.current.Board.DotColors.Contains(_acceptedColor);
        return valid;
    }

    void ILootConsumed.OnLootConsumed()
    {
        Debug.Log("[Level] AddTime!");
        _countdown.AddTime(3);
    }
}
