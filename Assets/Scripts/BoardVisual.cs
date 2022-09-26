using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System;
using AGK.GameGrids;
using Popper;

public class BoardVisual : MonoBehaviour
{
    public SpriteRenderer BoardBackground;

    [Header("Resources")]
    public Slot slotPrefab;
    public Dot dotPrefab;
    public Loot lootPrefab;
    [SerializeField] private ColorPalette colorPalette;

    [Header("Services")]
    public BoardCellGenerator generator;
    public CameraScreenFit gameView;

    public int RemainingColors { get => dotColors.Count; }
    private readonly float _cellRectSize = 1f;
    private LevelController _levelController;
    private LevelController LevelController => _levelController;

    [SerializeField] private float2 _boardRect;
    public float2 BoardRect { get => _boardRect; }

    public List<Slot> gridSlots;
    [SerializeField] private GameGrid2D<Slot> _grid;

    [SerializeField] private List<Color> dotColors;

    [SerializeField] private List<ISlotKey<ColorSlotKey>> _colorKeys;
    public List<Color> DotColors => dotColors;

    public Action OnBoardChanged;

    public void Construct(LevelController levelController)
    {
        _levelController = levelController;
    }

    public void UpdateColorList()
    {
        dotColors.Clear();
        for (int i = 0; i < LevelController.Config.BoardSize.x * LevelController.Config.BoardSize.y; i++)
        {
            if (dotColors.Contains(gridSlots[i].Keyhole.Color) == false)
            {
                if (gridSlots[i].IsActive == true)
                {
                    dotColors.Add(gridSlots[i].Keyhole.Color);
                }               
            }
        }
    }

    public Color GetRandomColor()
    {
        if (dotColors.Count > 0 )
        {
            int index = UnityEngine.Random.Range(0, dotColors.Count);
            ///Debug.Log(dotColors.Count);
            return dotColors[index];
        }
        return new Color();
    }

    // fills the grid with dot gameobjects
    public void LockWithDots(ColorPalette palette)
    {
        var bubblePool = new BubblePoolColors(palette);

        for (int i = 0; i < gridSlots.Count; i++)
        {
            //instantiating dots in grid
            gridSlots[i].Keyhole = Instantiate(dotPrefab, gridSlots[i].transform.position, Quaternion.identity) as Dot;

            //make dot gameobjects parent of slot
            gridSlots[i].Keyhole.transform.parent = gridSlots[i].transform;

            //assigning colors from the palette
            gridSlots[i].Keyhole.SetColor(bubblePool.GetRandomColor().Color);
        }
    }


    public void FillWithLoot()
    {
        int addedLoot = 0; 

        for (int i = 0; i < gridSlots.Count; i++)
        {
            bool hasLoot = UnityEngine.Random.value >= 0.6f;

            if (hasLoot && addedLoot < 5)
            {
                //instantiating dots in grid
                gridSlots[i].Loot = Instantiate(lootPrefab, gridSlots[i].transform.position, Quaternion.identity) as Loot;
                gridSlots[i].Loot.Construct(GameManager.current.Events, GameManager.current.UiManager);

                //make dot gameobjects parent of slot
                gridSlots[i].Loot.transform.parent = gridSlots[i].transform;
                addedLoot++;
            }
        }
    }

    private void GenerateCells()
    {
        float halfcell = _cellRectSize * 0.5f;

        generator.Construct(LevelController.Config, _cellRectSize, slotPrefab, gameObject.transform);
        _grid = new GameGrid2D<Slot>(LevelController.Config.BoardSize, generator.GenerateCells());
        gridSlots = generator.Slots;

        _boardRect = new float2(LevelController.Config.BoardSize.x * _cellRectSize + halfcell, LevelController.Config.BoardSize.y * _cellRectSize + halfcell);
    }


    public void OnLevelPhaseInitialize()
    {
        GenerateCells();

        LockWithDots(colorPalette);
        FillWithLoot();
        UpdateColorList();

        BoardBackground.size = new Vector2(BoardRect.x, BoardRect.y);
        OnBoardChanged?.Invoke();
    }
}
