using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// CellRootGenerator
/// </summary>
public class BoardCellGenerator : MonoBehaviour
{
    // instantiates slot objects in grid
    //[SerializeField] private int2 srBoardSize;
    //[SerializeField] private float srCellSize;
    //[SerializeField] private Slot srSlotPrefab;

    [SerializeField] private List<float3> cellRootCoordiantes;
    [SerializeField] private List<CellData> cellCoordinatesData;

    private int2 boardSize;
    private float cellSize;
    private Slot slotPrefab;
    public float3 origin;
    private List<Slot> _gridSlots;
    private Transform parent;

    public List<Slot> Slots { get => _gridSlots; }

    public void Construct(int2 boardSize, float cellSize, Slot slotPrefab, Transform origin)
    {
        this.boardSize = boardSize;
        this.cellSize = cellSize;
        this.slotPrefab = slotPrefab;
        this.origin = new float3(origin.position.x, origin.position.y, origin.position.z);
        parent = origin;

        _gridSlots = new List<Slot>();
    }

    public void GenerateCells()
    {
        int count = boardSize.x * boardSize.y;
        cellRootCoordiantes = new List<float3>(count);

        int index = 0;

        for (int i = 0; i < boardSize.x; i++)
        {
            float horizontalOffset = i * cellSize;

            horizontalOffset = horizontalOffset - (boardSize.x * 0.5f) + (cellSize * 0.5f);


            for (int j = 0; j < boardSize.y; j++)
            {
                float verticalOffset = j * cellSize;
                verticalOffset = verticalOffset - (boardSize.y * 0.5f) + (cellSize * 0.5f);

                float3 coord = new float3(origin.x + horizontalOffset,  origin.y + verticalOffset, origin.z);

                cellRootCoordiantes.Add(coord);

                index++;
            }
        }

        foreach (var coord in cellRootCoordiantes)
        {
            var root = Instantiate(slotPrefab, coord, quaternion.identity);
            root.transform.parent = parent;

            _gridSlots.Add(root);
        }
    }
}
