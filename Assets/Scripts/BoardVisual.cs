using AGK.GameGrids;
using AGK.GameGrids.CellGroups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private BoardCellSpawner _cellSpawner;

    public float2 BoardRect { get => _boardRect; }   

    public Action OnBoardChanged;

    private void Construct(LevelGrid grid, LevelController levelController)
    {
        _grid = grid;
        _colorPool = levelController.KeyPool;
    }

    private async Task GenerateBoard()
    {
        if (_cellSpawner != null)
            await _cellSpawner.DespawnCells();

        _cellSpawner = new BoardCellSpawner(_grid, CELL_SIZE, slotPrefab, dotPrefab, lootPrefab, _colorPool, gameObject.transform);
        _cellSpawner.SpawnCells();
              
        _boardRect = _cellSpawner.CellsBoundingBox;
        BoardBackground.size = new Vector2(BoardRect.x, BoardRect.y);

        OnBoardChanged?.Invoke();
    }

    public async Task SpawnAsync(LevelGrid grid, LevelController levelController)
    {
        Construct(grid, levelController);
        await GenerateBoard();
    }

    public async Task DespawnAsync()
    {

    }
}
