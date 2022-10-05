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
    public Slot slotPrefab;
    public Dot dotPrefab;
    public Loot lootPrefab;

    [Header("Services")]
    public CameraScreenFit gameView;
    private ISlotKeyPool<ColorSlotKey> _colorPool;

    
    private GameGrid2D<SlotData> _grid;
    private LevelController _levelController;
    private float2 _boardRect;   

    private LevelController LevelController => _levelController;    
    public float2 BoardRect { get => _boardRect; }   

    public Action OnBoardChanged;

    public void Construct(LevelGrid grid, LevelController levelController)
    {
        _levelController = levelController;
        _grid = grid;
        _colorPool = _levelController.KeyPool;
    }

    public void SpawnCells()
    {
        float halfcell = CELL_SIZE * 0.5f;
        
        int2 gridSize = _levelController.Config.BoardSize; //TODO: (AGK) get this from grid
        var cellSpawner = new BoardCellSpawner(_grid, gridSize, 1f, slotPrefab, dotPrefab, _colorPool, gameObject.transform);
        cellSpawner.GenerateCells();

        _boardRect = new float2(LevelController.Config.BoardSize.x * CELL_SIZE + halfcell, LevelController.Config.BoardSize.y * CELL_SIZE + halfcell);
    }


    public void OnLevelPhaseInitialize()
    {
        CheckIslands();

        BoardBackground.size = new Vector2(BoardRect.x, BoardRect.y);
        OnBoardChanged?.Invoke();
    }

    private void CheckIslands()
    {
        Debug.LogError($"Grid: {_grid == null} | {_grid.Nodes[0].SlotVisual.gameObject}");

        var islandFinder = new Islandfinder<GameGrid2D<SlotData>, SlotData>(_grid);
        islandFinder.RecalculateIslands();
        var islands = islandFinder.GetIslands(3);

        foreach (var island in islands)
        {
            var slot = _grid.GetNodeAt(island.Cells[0].Position);
            
            //connected slots
            List<Slot> connected = new List<Slot>();

            for (int i = 1; i < island.Cells.Count; i++)
            {
                connected.Add(_grid.GetNodeAt(island.Cells[i].Position).SlotVisual);
            }

            AddIslandDestructLoot(slot.SlotVisual, connected);
            Debug.Log($"[BoardVisual] {island.ToString()} --> {island.Cells[0].Position} ");
        }
    }

    private void AddIslandDestructLoot(Slot slot, List<Slot> connected)
    {
        bool confirmed = UnityEngine.Random.value >= 0.6f;

        if (true)
        {
            //instantiating dots in grid
            slot.Loot = Instantiate(lootPrefab, slot.transform.position, Quaternion.identity) as Loot;
            slot.Loot.Construct(GameManager.current.Events, GameManager.current.UiManager, connected);

            //make dot gameobjects parent of slot
            slot.Loot.transform.parent = slot.transform;
        }
    }
}
