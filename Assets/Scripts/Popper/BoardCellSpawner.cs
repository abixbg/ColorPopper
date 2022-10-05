using AGK.GameGrids;
//using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardCellSpawner
{
    private GameGrid2D<SlotData> grid;

    private readonly float2 dimentions;
    private readonly float cellWorldSize;
    private readonly SlotVisual slotPrefab;
    private readonly Dot dotPrefab;
    public readonly float3 origin;
    private readonly Transform parent;
    private ISlotKeyPool<ColorSlotKey> keyPool;

    public float2 CellsBoundingBox => dimentions;
    private readonly List<SlotVisual> slotVisuals = new List<SlotVisual>();

    public BoardCellSpawner(GameGrid2D<SlotData> grid, float cellWorldSize, SlotVisual slotPrefab, Dot dotPrefab, ISlotKeyPool<ColorSlotKey> keyPool, Transform origin)
    {
        this.grid = grid;
        dimentions = new float2(grid.Size.x * cellWorldSize + cellWorldSize * 0.5f, grid.Size.y * cellWorldSize + cellWorldSize * 0.5f);
        this.cellWorldSize = cellWorldSize;
        this.slotPrefab = slotPrefab;
        this.dotPrefab = dotPrefab;
        this.origin = new float3(origin.position.x, origin.position.y, origin.position.z);
        this.keyPool = keyPool;

        parent = origin;
    }

    public void GenerateCells()
    {
        int index = 0;
        List<PositionData> posData = new List<PositionData>();

        slotVisuals.Clear();

        for (int i = 0; i < grid.Size.x; i++)
        {
            float horizontalOffset = i * cellWorldSize;

            horizontalOffset = horizontalOffset - (grid.Size.x * 0.5f) + (cellWorldSize * 0.5f);


            for (int j = 0; j < grid.Size.y; j++)
            {
                float verticalOffset = j * cellWorldSize;
                verticalOffset = verticalOffset - (grid.Size.y * 0.5f) + (cellWorldSize * 0.5f);

                float3 vPos = new float3(origin.x + horizontalOffset, origin.y + verticalOffset, origin.z);
                GridPosition gPos = new GridPosition(i, j);

                var data = new PositionData(gPos, vPos);

                posData.Add(data);

                index++;
            }
        }

        foreach (var pos in posData)
        {
            var slotVisual = Object.Instantiate(slotPrefab, pos.VisualPosition, quaternion.identity);
            slotVisual.Construct(grid, pos.Position, parent);
            slotVisuals.Add(slotVisual);
        }
    }


    public void AddContent()
    {
        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var node = grid.Nodes[i];
            AddLockColorDot(slotVisuals[i], node);
        }
    }

    // fills the grid with dot gameobjects
    private void AddLockColorDot(SlotVisual slotVisual, SlotData slot)
    {
        //instantiating dots in grid
        slotVisual.Keyhole = Object.Instantiate(dotPrefab, slotVisual.transform.position, Quaternion.identity) as Dot;

        //make dot gameobjects parent of slot
        slotVisual.Keyhole.transform.parent = slotVisual.transform;

        Color color = keyPool.GetRandom().Color;

        //assigning colors from the palette
        slotVisual.Keyhole.SetColor(color);
        slot.Content = new ColorSlotKey(color);
    }

    private struct PositionData
    {
        [SerializeField] private GridPosition gridPos;
        [SerializeField] private float3 visualPos;

        public GridPosition Position { get => gridPos; set => gridPos = value; }
        public float3 VisualPosition { get => visualPos; set => visualPos = value; }

        public PositionData(GridPosition gridPos, float3 visualPos)
        {
            this.gridPos = gridPos;
            this.visualPos = visualPos;
        }
    }
}
