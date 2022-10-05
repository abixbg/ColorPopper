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
    public Loot lootPrefab;

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

    //private void AddLoot()
    //{
    //    var islandFinder = new Islandfinder<GameGrid2D<SlotData>, SlotData>(_grid);
    //    islandFinder.RecalculateIslands();
    //    var islands = islandFinder.GetIslands(3);

    //    Debug.LogError($"Islands: {islands.Count}");

    //    foreach (var island in islands)
    //    {
    //        var slot = _grid.GetNodeAt(island.Cells[0].Position);
            
    //        //connected slots
    //        List<SlotVisual> connected = new List<SlotVisual>();

    //        for (int i = 1; i < island.Cells.Count; i++)
    //        {
    //            connected.Add(_grid.GetNodeAt(island.Cells[i].Position).SlotVisual);
    //        }

    //        AddIslandDestructLoot(slot, connected);
    //        Debug.LogWarning($"[BoardVisual] {island.ToString()} --> {island.Cells[0].Position} ");
    //    }
    //}

    //private void AddIslandDestructLoot(SlotData slot, List<SlotVisual> connected)
    //{
    //    bool confirmed = UnityEngine.Random.value >= 0.6f;

    //    if (true)
    //    {
    //        //instantiating dots in grid
    //        slot.SlotVisual.Loot = Instantiate(lootPrefab, slot.SlotVisual.transform.position, Quaternion.identity) as Loot;
    //        slot.SlotVisual.Loot.Construct(GameManager.current.Events, GameManager.current.UiManager, connected);

    //        //make dot gameobjects parent of slot
    //        slot.SlotVisual.Loot.transform.parent = slot.SlotVisual.transform;
    //    }
    //}
}
