using Pathfinding;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PrototypeGrid : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int2 gridSize;
    [SerializeField] private int2 start;
    [SerializeField] private int2 end;

    public PathfindingGrid grid;
    public PathfindingGrid Grid => grid;

    private Pathfinder pathfinder;

    public List<PathNode> Path => pathfinder.FindPath(start, end);

    void Start()
    {
        grid = new PathfindingGrid(gridSize);
        pathfinder = new Pathfinder(grid);

        MakeUnwalkable();
    }

    private void MakeUnwalkable()
    {
        grid.GetNodeAt(new int2(0, 3)).SetUnwalkable();
        grid.GetNodeAt(new int2(1, 3)).SetUnwalkable();
        grid.GetNodeAt(new int2(2, 3)).SetUnwalkable();
        grid.GetNodeAt(new int2(3, 3)).SetUnwalkable();
        grid.GetNodeAt(new int2(5, 3)).SetUnwalkable();
        grid.GetNodeAt(new int2(6, 3)).SetUnwalkable();
        grid.GetNodeAt(new int2(7, 3)).SetUnwalkable();
    }
}
