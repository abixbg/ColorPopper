using EventBroadcast;
using Popper;
using Popper.Events;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

public class LevelController :
    ISlotInput,
    ISlotStateChanged,
    ILootPicked,
    ILootConsumed
{
    private readonly Stopwatch _stopwatch;
    private readonly BoardVisual _boardVisual;
    private readonly GeneratorLoot _generatorLoot;

    private LevelConfigData _config;
    private LevelGrid _grid;  
    private BubblePoolColors _keyPool;
    private GeneratorContentColor _generatorContent;
    private float bonusTime = 0f;

    private IEventBus Events => GameManager.current.Events;
    public LevelConfigData Config => _config;
    public LevelGrid Grid => _grid;
    public CellMatchExact<ColorSlotKey> AcceptRules { get; private set; }
    public Stopwatch Stopwatch => _stopwatch;
    private GeneratorContentColor Content => _generatorContent;
    private GeneratorLoot Loot => _generatorLoot;
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
        
        _generatorLoot = new GeneratorLoot();

        //_keyPool = new BubblePoolColors(_config.Pallete);
        //_generatorContent = new GeneratorContentColor(KeyPool);


        ////Generate CellData
        //_grid = new LevelGrid(_config.BoardSize, LevelGrid.Generate(_config.BoardSize));



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
            _keyPool = new BubblePoolColors(_config.Pallete);
            _generatorContent = new GeneratorContentColor(KeyPool);
            //Generate CellData
            _grid = new LevelGrid(_config.BoardSize, LevelGrid.Generate(_config.BoardSize));

            AcceptRules = new CellMatchExact<ColorSlotKey>(KeyPool.GetRandom());

            //Reset
            Grid.ResetCellsContent();

            //Generate new
            Content.AddContent(Grid);
            Loot.AddLoot(Grid);
        }
        else
        {
            Grid.ResetCellsState();
            KeyPool.Reset();
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
        await _boardVisual.SpawnAsync(Grid, this);
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

    private void SwitchAcceptedContent()
    {
        AcceptRules = new CellMatchExact<ColorSlotKey>(_keyPool.GetRandomNew(AcceptRules.Current));
        Events.Broadcast<IAcceptedColorChanged>(s => s.OnAcceptedColorChange(AcceptRules.Current.Color));
    }

    private bool ValidBoard()
    {
        bool valid = HaveKeyHoleOnBoard(AcceptRules.Current);
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
