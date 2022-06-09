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

    private int2 boardSize;
    private float cellSize;
    private Slot slotPrefab;
    private Transform origin;
    private List<Slot> _gridSlots;

    public List<Slot> Slots { get => _gridSlots; }

    public void Construct(int2 boardSize, float cellSize, Slot slotPrefab, Transform origin)
    {
        this.boardSize = boardSize;
        this.cellSize = cellSize;
        this.slotPrefab = slotPrefab;
        this.origin = origin;

        _gridSlots = new List<Slot>();
    }

    public void GenerateCells()
    {
        int count = boardSize.x * boardSize.y;
        cellRootCoordiantes = new List<float3>(count);

        int index = 0;

        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                Vector3 pos = new Vector3(j * cellSize, i * cellSize, 0) + origin.position;

                float3 coord = new float3(pos.x, pos.y, pos.z);

                cellRootCoordiantes.Add(coord);

                ////instantiating dots in grid
                //gridSlots[index] = Instantiate(slotPrefab, pos, Quaternion.identity) as Slot;

                ////make slots gameobjects parent of grid
                //gridSlots[index].transform.parent = gameObject.transform;

                index++;
            }
        }

        GenerateCellRoots();
    }

    private void GenerateCellRoots()
    {
        foreach (var coord in cellRootCoordiantes)
        {
            var root = Instantiate(slotPrefab, coord, quaternion.identity);
            root.transform.parent = origin;

            _gridSlots.Add(root);
        }
    }
}
