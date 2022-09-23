using Pathfinding;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PrototypeGrid : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int2 gridSize;
    [SerializeField] private GridPosition start;
    [SerializeField] private GridPosition end;

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
        grid.GetNodeAt(new GridPosition(0, 3)).SetBlocked();
        grid.GetNodeAt(new GridPosition(1, 3)).SetBlocked();
        grid.GetNodeAt(new GridPosition(2, 3)).SetBlocked();
        grid.GetNodeAt(new GridPosition(3, 3)).SetBlocked();
        grid.GetNodeAt(new GridPosition(5, 3)).SetBlocked();
        grid.GetNodeAt(new GridPosition(6, 3)).SetBlocked();
        grid.GetNodeAt(new GridPosition(7, 3)).SetBlocked();
    }
}
