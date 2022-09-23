using AGK.GameGrids;
using AGK.GameGrids.Pathfinding;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PrototypeGrid : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int2 gridSize;
    [SerializeField] private GridPosition start;
    [SerializeField] private GridPosition end;

    public PathfindingGrid<PathNode> grid;
    public PathfindingGrid<PathNode> Grid => grid;

    private Pathfinder<PathfindingGrid<PathNode>, PathNode> pathfinder;

    public List<PathNode> Path => pathfinder.FindPath(start, end);

    void Start()
    {
        grid = new PathfindingGrid<PathNode>(gridSize);
        pathfinder = new Pathfinder<PathfindingGrid<PathNode>, PathNode>(grid);

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
