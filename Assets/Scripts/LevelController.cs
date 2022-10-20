using EventBroadcast;
using Popper;
using Popper.Events;
using System.Threading.Tasks;
using UnityEngine;

public class LevelController :
    ISlotInput,
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
    private readonly BoardVisual _boardVisual;
    private readonly GeneratorContentColor _generatorContent;
    private readonly GeneratorLoot _generatorLoot;

    public LevelConfigData Config => _config;
    public LevelGrid Grid => _grid;
    public CellMatchExact<ColorSlotKey> AcceptedContent { get; private set; }
    public Countdown Countdown => _countdown;
    public Stopwatch Stopwatch => _stopwatch;
    private GeneratorContentColor Content => _generatorContent;
    private GeneratorLoot Loot => _generatorLoot;
    public BubblePoolColors KeyPool => _keyPool;
    public float TimeRemaining => _countdown.TimeRemaining;   


    public int BoardCellremaining
    {
        get
        {
            return _grid.Nodes.FindAll(s => s.IsActive == true).Count;
        }
    }

    public LevelController(LevelConfigData levelConfig, EventBus levelEvents, GameClock clock, BoardVisual boardVisual)
    {
        _config = levelConfig;
        _events = levelEvents;
        _boardVisual = boardVisual;
        _countdown = new Countdown(levelConfig.TimeSec);
        _stopwatch = new Stopwatch(clock);
        _keyPool = new BubblePoolColors(levelConfig.Pallete);
        _generatorContent = new GeneratorContentColor(KeyPool);
        _generatorLoot = new GeneratorLoot();

        //Generate CellData
        _grid = new LevelGrid(levelConfig.BoardSize, LevelGrid.Generate(levelConfig.BoardSize));

        AcceptedContent = new CellMatchExact<ColorSlotKey>(_keyPool.GetRandom());

        _events.Subscribe<ILootPicked>(this);
        _events.Subscribe<ILootConsumed>(this);
        _events.Subscribe<ISlotInput>(this);
        _events.Subscribe<ISlotStateChanged>(this);
    }

    #region ISlotClicked
    void ISlotInput.OnClicked(SlotData slot)
    {
        bool accepted = AcceptedContent.IsAccepted(slot.Content);

        if (slot.IsActive)
        {
            if (accepted)
                slot.OpenSlot();
            else
                slot.BreakSlot();
        }
    }
    #endregion

    #region ISlotStateChanged
    void ISlotStateChanged.OnSlotOpen(SlotData slot)
    {
        var key = (ColorSlotKey)slot.Content;

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

    void ISlotStateChanged.OnSlotBreak(SlotData slot)
    {
        var key = (ColorSlotKey)slot.Content;

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

    void ISlotStateChanged.OnSlotOpenAuto(SlotData _)
    {

    }
    #endregion

    #region ILootPicked
    async void ILootPicked.OnLootActivate(SlotLoot loot)
    {
        //TODO: Implement loot Resolver or split to interfaces
        await loot.ActivateEffect();
    }
    void ILootPicked.OnLootDiscard(SlotLoot _)
    {

    }

    #endregion

    #region ILootConsumed
    void ILootConsumed.OnLootConsumed(SlotLoot _)
    {
        Debug.Log("[Level] AddTime!");
        _countdown.AddTime(3);
    }
    #endregion


    private void InitialiizeLevelData(bool generateNewContent = true)
    {
        Debug.Log("[Level] Initialilze!");

        Grid.ResetCellsState();
        KeyPool.Reset();

        if (generateNewContent)
        {
            //Reset
            Grid.ResetCellsContent();

            //Generate new
            Content.AddContent(Grid);
            Loot.AddLoot(Grid);
        }

        //Set Accepted color
        SwitchAcceptedContent();

        //Reset Time
        Stopwatch.Reset();
        Countdown.Reset(Config.TimeSec);
        //#TODO
    }


    public void StartLevel()
    {
        Debug.Log("[Level] Start!");

        //Start timers
        Stopwatch.SetActive(true);
    }

    public async Task SetVisualReadyAsync()
    {
        //Spawn Visual
        await _boardVisual.SpawnAsync(Grid, this);
    }

    public async void RestartLevel()
    {
        InitialiizeLevelData(false);
        await SetVisualReadyAsync();    
        StartLevel();
    }

    public async void QuickStartLevel()
    {
        InitialiizeLevelData();
        await SetVisualReadyAsync();
        StartLevel();
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

    private bool HaveKeyHoleOnBoard(ColorSlotKey key)
    {
        foreach (var slot in _grid.Nodes)
        {
            if (slot.Content.IsMatch(key) && slot.IsActive)
            {
                //Debug.LogError($"Found: {slot.SlotVisual}", slot.SlotVisual.gameObject);
                return true;
            }

        }

        return false;
    }
}
