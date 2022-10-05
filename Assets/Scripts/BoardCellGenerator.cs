//using AGK.GameGrids;
//using System.Collections.Generic;
//using Unity.Mathematics;
//using UnityEngine;

///// <summary>
///// CellRootGenerator
///// </summary>
//public class BoardCellGenerator : MonoBehaviour
//{
//    [SerializeField] public List<CellData> cellsData;

//    private int2 boardSize;
//    private float cellSize;
//    private Slot slotPrefab;
//    public float3 origin;
//    private List<Slot> _gridSlots;
//    private Transform parent;



//    public void Construct(LevelConfigData levelData, float cellSize, Slot slotPrefab, Transform origin)
//    {
//        this.boardSize = levelData.BoardSize;
//        this.cellSize = cellSize;
//        this.slotPrefab = slotPrefab;
//        this.origin = new float3(origin.position.x, origin.position.y, origin.position.z);
//        parent = origin;
//        _gridSlots = new List<Slot>();
//    }

//    public List<Slot> GenerateCells()
//    {
//        int index = 0;

//        for (int i = 0; i < boardSize.x; i++)
//        {
//            float horizontalOffset = i * cellSize;

//            horizontalOffset = horizontalOffset - (boardSize.x * 0.5f) + (cellSize * 0.5f);


//            for (int j = 0; j < boardSize.y; j++)
//            {
//                float verticalOffset = j * cellSize;
//                verticalOffset = verticalOffset - (boardSize.y * 0.5f) + (cellSize * 0.5f);

//                float3 vPos = new float3(origin.x + horizontalOffset, origin.y + verticalOffset, origin.z);

//                GridPosition gPos = new GridPosition(i, j);

//                cellsData.Add(new CellData(gPos, vPos));

//                index++;
//            }
//        }

//        foreach (var data in cellsData)
//        {
//            var slot = Instantiate(slotPrefab, data.VisualPosition, quaternion.identity);
//            slot.transform.parent = parent;
//            slot.Construct(data);

//            _gridSlots.Add(slot);
//        }

//        return _gridSlots;
//    }
//}
