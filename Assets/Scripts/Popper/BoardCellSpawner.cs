using AGK.GameGrids;
//using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardCellSpawner
{
    private GameGrid2D<SlotData> grid;

    private readonly int2 boardSize;
    private readonly float cellWorldSize;
    private readonly Slot slotPrefab;
    private readonly Dot dotPrefab;
    public readonly float3 origin;
    private readonly Transform parent;
    private ISlotKeyPool<ColorSlotKey> keyPool;

    public BoardCellSpawner(GameGrid2D<SlotData> grid, int2 boardSize, float cellWorldSize, Slot slotPrefab, Dot dotPrefab, ISlotKeyPool<ColorSlotKey> keyPool, Transform origin)
    {
        this.grid = grid;
        this.boardSize = boardSize;
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
        List<Slot> gridSlots = new List<Slot>();
        List<PositionData> posData = new List<PositionData>();

        for (int i = 0; i < boardSize.x; i++)
        {
            float horizontalOffset = i * cellWorldSize;

            horizontalOffset = horizontalOffset - (boardSize.x * 0.5f) + (cellWorldSize * 0.5f);


            for (int j = 0; j < boardSize.y; j++)
            {
                float verticalOffset = j * cellWorldSize;
                verticalOffset = verticalOffset - (boardSize.y * 0.5f) + (cellWorldSize * 0.5f);

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
            slotVisual.transform.parent = parent;
            slotVisual.Construct(grid, pos.Position);
            LockWithDot(slotVisual);

            gridSlots.Add(slotVisual);
        }

        for (int i = 0; i < grid.Nodes.Count; i++)
        {
            var node = grid.Nodes[i];
            node.SetVisual(gridSlots[i]);
        }
    }

    // fills the grid with dot gameobjects
    public void LockWithDot(Slot slot)
    {
        //instantiating dots in grid
        slot.Keyhole = Object.Instantiate(dotPrefab, slot.transform.position, Quaternion.identity) as Dot;

        //make dot gameobjects parent of slot
        slot.Keyhole.transform.parent = slot.transform;

        //assigning colors from the palette
        slot.Keyhole.SetColor(keyPool.GetRandom().Color);
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
