using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Pathfinder
{
    private const int COST_STRAIGHT = 10;
    private const int COST_DIAGONAL = 14;

    private PathfindingGrid grid;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinder(PathfindingGrid grid)
    {
        this.grid = grid;
    }

    public List<PathNode> FindPath(int2 start, int2 end)
    {
        PathNode startNode = grid.GetNodeAt(start);
        PathNode endNode = grid.GetNodeAt(end);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        foreach (var node in grid.Nodes)
        {
            node.gCost = int.MaxValue;
            node.CalculateFCost();
            node.CameFrom = null;
        }

        //Start
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        Debug.LogWarning($"h={startNode.hCost} | f={startNode.fCost} | g={startNode.gCost}");

        //others
        while (openList.Count > 0)
        {           
            PathNode currentNode = GetLowestFCostNode(openList);

            Debug.LogError($"Check: {currentNode.LocString}");

            if (currentNode == endNode)
            {
                //reached final
                Debug.Log("Done!");
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neughbour in GetAllNeighbours(currentNode))
            {
                if (closedList.Contains(neughbour))
                    continue;

                if (!neughbour.Walkable)                  
                {
                    closedList.Add(neughbour);
                    continue;
                }

                Debug.LogWarning($"[{neughbour.Location.x},{neughbour.Location.y}] === h={neughbour.hCost} | f={neughbour.fCost}");

                int provisionalGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neughbour);

                if (provisionalGCost < neughbour.gCost)
                {
                    neughbour.CameFrom = currentNode;
                    neughbour.gCost = provisionalGCost;
                    neughbour.hCost = CalculateDistanceCost(neughbour, endNode);
                    neughbour.CalculateFCost();

                    if (!openList.Contains(neughbour))
                        openList.Add(neughbour);
                }
            }
        }

        //Out of nodes
        Debug.LogError("No path");
        return null;
    }

    private List<PathNode> GetAllNeighbours(PathNode currentNode)
    {
        var neighbours = new List<PathNode>();

        Debug.LogError($"GRID --> {grid.Size}");

        if (currentNode.Location.x - 1 >= 0)
        {
            //SW
            if (currentNode.Location.y - 1 >= 0)
                neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x - 1, currentNode.Location.y - 1)));

            //W
            neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x - 1, currentNode.Location.y)));

            //NW
            if (currentNode.Location.y + 1 >= 0)
                neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x - 1, currentNode.Location.y + 1)));
        }

        //S
        if (currentNode.Location.y - 1 >= 0)
            neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x, currentNode.Location.y - 1)));

        //N
        if (currentNode.Location.y + 1 >= 0)
            neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x, currentNode.Location.y + 1)));

        if (currentNode.Location.x + 1 < grid.Size.x)
        {
            //SE
            if (currentNode.Location.y - 1 >= 0)
                neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x + 1, currentNode.Location.y - 1)));

            //E
            neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x + 1, currentNode.Location.y)));

            //NE
            if (currentNode.Location.y + 1 < grid.Size.y)
                neighbours.Add(grid.GetNodeAt(new int2(currentNode.Location.x + 1, currentNode.Location.y + 1)));
        }
        return neighbours;
    }

    private List<PathNode> CalculatePath(PathNode endnode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endnode);

        PathNode currentNode = endnode;

        while (currentNode.CameFrom != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.CameFrom;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDist = (int)MathF.Abs(a.Location.x - b.Location.x);
        int yDist = (int)MathF.Abs(a.Location.y - b.Location.y);
        int remaining = (int)MathF.Abs(xDist - yDist);

        int dist = COST_DIAGONAL * Mathf.Min(xDist, yDist) + COST_STRAIGHT * remaining;

        Debug.Log($"{a.LocString} --> {b.LocString}| Dist={dist}");

        return dist;
    }

    private PathNode GetLowestFCostNode(List<PathNode> nodes)
    {
        var lowest = nodes[0];

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].fCost < lowest.fCost)
            {
                lowest = nodes[i];
            }
        }

        return lowest;
    }
}
