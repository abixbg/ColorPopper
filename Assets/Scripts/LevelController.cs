using AGK.GameGrids;
using EventBroadcast;
using Popper;
using Popper.Events;
using UnityEngine;

public class LevelController :
    ISlotClicked,
    ISlotStateChanged,
    ILootPicked,
    ILootConsumed
{
    private readonly LevelConfigData _config;
    private readonly LevelGrid _grid;
    private readonly Countdown _countdown;
    private readonly Stopwatch _stopwatch;
    private readonly BubblePoolColors _keyPool;  
    private readonly IEventBus _events;

    private BoardVisual _board;

    public LevelConfigData Config => _config;
    public LevelGrid Grid => _grid;
    public CellMatchExact<ColorSlotKey> AcceptedContent { get; private set; }   
    public Countdown Countdown => _countdown;
    public Stopwatch Stopwatch => _stopwatch;
    public BubblePoolColors KeyPool => _keyPool;
    public float TimeRemaining => _countdown.TimeRemaining;

    public int BoardCellremaining
    {
        get
        {
            return _grid.Nodes.FindAll(s => s.IsActive == true).Count;
        }
    }

    public LevelController(LevelConfigData levelConfig, EventBus levelEvents, GameClock clock)
    {
        _config = levelConfig;
        _events = levelEvents;
        _countdown = new Countdown(levelConfig.TimeSec);
        _stopwatch = new Stopwatch(clock);
        _stopwatch.SetActive(true);
        _keyPool = new BubblePoolColors(levelConfig.Pallete);

        _grid = new LevelGrid(levelConfig.BoardSize, LevelGrid.Generate(levelConfig.BoardSize));
        
        AcceptedContent = new CellMatchExact<ColorSlotKey>(_keyPool.GetRandom());

        _events.Subscribe<ILootPicked>(this);
        _events.Subscribe<ILootConsumed>(this);
        _events.Subscribe<ISlotClicked>(this);
        _events.Subscribe<ISlotStateChanged>(this);
    }

    #region ISlotClicked
    void ISlotClicked.OnSlotClicked(Slot slot)
    {
        bool accepted = AcceptedContent.IsAccepted(slot.Keyhole);

        if (accepted)
            slot.OpenSlot();
        else
            slot.BreakSlot();
    }
    #endregion

    #region ISlotStateChanged
    void ISlotStateChanged.OnSlotOpen(Slot slot)
    {
        var key = new ColorSlotKey(slot.Keyhole.Color);

        if (!HaveKeyHoleOnBoard(key))
        {
            GameManager.current.Level.KeyPool.Remove(key);
        }

        bool validBoard = ValidBoard();
        bool randomSwitch = UnityEngine.Random.value <= 0.05f;
        bool hasLoot = slot.Loot != null;

        if (BoardCellremaining < 1)
        {
            Debug.Log($"[Level] Completed!");
            _stopwatch.SetActive(false);
            return;
        }

        if (!validBoard || randomSwitch || hasLoot)
            SwitchAcceptedContent();
    }

    void ISlotStateChanged.OnSlotBreak(Slot slot)
    {
        var key = new ColorSlotKey(slot.Keyhole.Color);

        if (!HaveKeyHoleOnBoard(key))
        {
            GameManager.current.Level.KeyPool.Remove(key);
        }

        if (BoardCellremaining < 1)
        {
            Debug.LogAssertion($"LevelCompleted!");
            _stopwatch.SetActive(false);
            return;
        }

        SwitchAcceptedContent();
    }
    #endregion

    #region ILootPicked
    void ILootPicked.OnLootPicked(Loot loot)
    {
        foreach (var slot in loot.ConnectedSlots)
        {
            slot.Loot = null;
            slot.OpenSlot();
        }
    }
    #endregion

    #region ILootConsumed
    void ILootConsumed.OnLootConsumed(Loot loot)
    {
        Debug.Log("[Level] AddTime!");
        _countdown.AddTime(3);
    }
    #endregion


    public void StartLevel()
    {
        Debug.Log("[Level] Start!");
        SwitchAcceptedContent();
    }

    private void SwitchAcceptedContent()
    {
        AcceptedContent = new CellMatchExact<ColorSlotKey>(_keyPool.GetRandomNew(AcceptedContent.Current));
        _events.Broadcast<IAcceptedColorChanged>(s => s.OnAcceptedColorChange(AcceptedContent.Current.Color));
    }

    private bool ValidBoard()
    {
        bool valid = HaveKeyHoleOnBoard(AcceptedContent.Current);
        return valid;
    }

    public bool HaveKeyHoleOnBoard(ColorSlotKey key)
    {
        foreach (var slot in _grid.Nodes)
        {
            if (slot.SlotVisual.Keyhole.Color == key.Color && slot.IsActive)
            {
                Debug.LogError($"Found: {slot.SlotVisual}", slot.SlotVisual.gameObject);
                return true;
            }

        }

        return false;
    }
}
