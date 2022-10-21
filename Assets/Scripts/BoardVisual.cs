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
    
    private GameGrid2D<SlotData> _grid;
    private float2 _boardRect;

    private BoardCellSpawner _cellSpawner;

    public float2 BoardRect { get => _boardRect; }   

    public Action OnBoardChanged;

    private void Construct(LevelGrid grid)
    {
        _grid = grid;
    }

    private async Task GenerateBoard()
    {
        if (_cellSpawner != null)
            await _cellSpawner.DespawnCells();

        _cellSpawner = new BoardCellSpawner(_grid, CELL_SIZE, slotPrefab, dotPrefab, lootPrefab, gameObject.transform);            
        _boardRect = _cellSpawner.CellsBoundingBox;
        BoardBackground.size = new Vector2(BoardRect.x, BoardRect.y);
        OnBoardChanged?.Invoke(); //TODO: this is only neded for safe space modification

        await _cellSpawner.SpawnCellsAsync();
    }

    public async Task SpawnAsync(LevelGrid grid)
    {
        Construct(grid);
        await GenerateBoard();
    }

    public async Task DespawnAsync()
    {

    }
}
