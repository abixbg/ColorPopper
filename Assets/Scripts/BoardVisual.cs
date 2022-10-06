using AGK.GameGrids;
using AGK.GameGrids.CellGroups;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardVisual : MonoBehaviour
{
    private const float CELL_SIZE = 1f;

    public SpriteRenderer BoardBackground;

    [Header("Resources")]
    public SlotVisual slotPrefab;
    public Dot dotPrefab;
    public LootVisual lootPrefab;

    [Header("Services")]
    public CameraScreenFit gameView;
    private ISlotKeyPool<ColorSlotKey> _colorPool;
    
    private GameGrid2D<SlotData> _grid;
    private float2 _boardRect;   

    public float2 BoardRect { get => _boardRect; }   

    public Action OnBoardChanged;

    public void Construct(LevelGrid grid, LevelController levelController)
    {
        _grid = grid;
        _colorPool = levelController.KeyPool;
    }

    public void GenerateBoard()
    {       
        var contentGenerator = new GeneratorContentColor(_grid, _colorPool);
        var lootGenerator = new GeneratorLoot(_grid);
        
        contentGenerator.AddContent();
        lootGenerator.GenerateLoot();

        var cellSpawner = new BoardCellSpawner(_grid, CELL_SIZE, slotPrefab, dotPrefab, lootPrefab, _colorPool, gameObject.transform);
        cellSpawner.SpawnCells();
              
        _boardRect = cellSpawner.CellsBoundingBox;
        BoardBackground.size = new Vector2(BoardRect.x, BoardRect.y);
    }

    public void OnLevelPhaseInitialize()
    {
        //AddLoot();
        OnBoardChanged?.Invoke();
    }
}
