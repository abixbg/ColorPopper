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
    private readonly Stopwatch _stopwatch;
    private readonly BoardVisual _boardVisual;

    private LevelConfigData _config;
    private LevelGrid _grid;
    private BubblePoolColors _keyPool;
    private float bonusTime = 0f;

    private IEventBus Events => GameManager.current.Events;
    public LevelConfigData Config => _config;
    public LevelGrid Grid => _grid;
    public CellMatchExact<ColorSlotKey> AcceptRules { get; private set; }
    public Stopwatch Stopwatch => _stopwatch;
    public BubblePoolColors KeyPool => _keyPool;
    public float TimeRemaining => Config.TimeSec + bonusTime - Stopwatch.TimeSec;


    public int BoardCellremaining
    {
        get
        {
            return _grid.Nodes.FindAll(s => s.IsActive == true).Count;
        }
    }

    public LevelController(GameClock clock, BoardVisual boardVisual)
    {
        //_config = levelConfig;
        _boardVisual = boardVisual;

        _stopwatch = new Stopwatch(clock);
        _stopwatch.ValueUpdated += OnStopwatchUpdate;

        Events.Subscribe<ILootPicked>(this);
        Events.Subscribe<ILootConsumed>(this);
        Events.Subscribe<ISlotInput>(this);
        Events.Subscribe<ISlotStateChanged>(this);
    }

    #region ISlotClicked
    void ISlotInput.OnClicked(SlotData slot)
    {
        bool accepted = AcceptRules.IsAccepted(slot.Content);

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
        if (CheckLevelComplete())
            return;

        ValidateKeyPool(slot.Content);

        bool validBoard = _grid.HaveKeyHoleOnBoard(AcceptRules.Current);
        bool randomSwitch = UnityEngine.Random.value <= 0.05f;
        bool hasLoot = slot.Loot != null;

        if (!validBoard || randomSwitch || hasLoot)
            SwitchAcceptedContent();
    }

    void ISlotStateChanged.OnSlotBreak(SlotData slot)
    {
        if (CheckLevelComplete())
            return;

        ValidateKeyPool(slot.Content);
        SwitchAcceptedContent();
    }

    void ISlotStateChanged.OnSlotOpenAuto(SlotData slot)
    {
        if (CheckLevelComplete())
            return;

        ValidateKeyPool(slot.Content);
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
        bonusTime += 3;
    }
    #endregion


    private void OnStopwatchUpdate()
    {
        LevelTimeData data = new LevelTimeData(Stopwatch.TimeSec, TimeRemaining, bonusTime);
        Events.Broadcast<ILevelStopwatchUpdate>(s => s.OnValueUpdate(data));
    }

    private void InitialiizeLevelData(LevelConfigData config, bool generateNewContent = true)
    {
        Debug.Log("[Level] Initialilze!");

        if (generateNewContent)
        {
            _config = config;

            //Generate empty grid
            _grid = new LevelGrid(_config.BoardSize, LevelGrid.Generate(_config.BoardSize));

            //Generate new
            var genPool = new BubblePoolColors(_config.Pallete);
            Grid.AddContent(new GeneratorContentColor(genPool));
            Grid.AddLoot(new GeneratorLoot());

            _keyPool = new BubblePoolColors(Grid.AllKeys);
            AcceptRules = new CellMatchExact<ColorSlotKey>((ColorSlotKey)KeyPool.GetRandom());
        }
        else
        {
            Grid.ResetCellsState();
            _keyPool = new BubblePoolColors(Grid.AllKeys);
        }

        //Set Accepted color
        SwitchAcceptedContent();

        //Reset Time
        bonusTime = 0f;
        Stopwatch.Reset();
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
        await _boardVisual.SpawnAsync(Grid);
    }

    public async void RestartLevel()
    {
        InitialiizeLevelData(_config, false);
        await SetVisualReadyAsync();
        StartLevel();
    }

    public async void QuickStartLevel(LevelConfigData levelConfig)
    {
        InitialiizeLevelData(levelConfig);
        await SetVisualReadyAsync();
        StartLevel();
    }

    #region Level Rules
    private bool CheckLevelComplete()
    {
        if (BoardCellremaining < 1)
        {
            Debug.LogAssertion($"LevelCompleted!");
            Stopwatch.SetActive(false);
            Events.Broadcast<ILevelStateUpdate>(s => s.OnLevelCompleted());
            return true;
        }
        return false;
    }
    #endregion

    private void ValidateKeyPool(SlotContent key)
    {
        if (!_grid.HaveKeyHoleOnBoard(key))
        {
            KeyPool.Remove((ColorSlotKey)key);
        }
    }

    private void SwitchAcceptedContent()
    {
        AcceptRules = new CellMatchExact<ColorSlotKey>((ColorSlotKey)_keyPool.GetRandomNew(AcceptRules.Current));
        Events.Broadcast<IAcceptedColorChanged>(s => s.OnAcceptedColorChange(AcceptRules.Current));
    }
}
