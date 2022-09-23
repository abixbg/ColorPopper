using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Pathfinder<TNode> where TNode : IPathNode, new()
{
    private const int COST_STRAIGHT = 10;
    private const int COST_DIAGONAL = 14;

    private readonly PathfindingGrid<TNode> grid;

    public Pathfinder(PathfindingGrid<TNode> grid)
    {
        this.grid = grid;
    }

    public List<TNode> FindPath(GridPosition start, GridPosition end)
    {
        TNode startNode = grid.GetNodeAt(start);
        TNode endNode = grid.GetNodeAt(end);

        var openList = new List<TNode> { startNode };
        var closedList = new List<TNode>();

        foreach (var node in grid.Nodes)
        {
            node.CostG = int.MaxValue;
            node.CameFrom = null;
        }

        //Start
        startNode.CostG = 0;
        startNode.CostH = CalculateDistanceCost(startNode, endNode);

        Debug.LogWarning($"h={startNode.CostH} | f={startNode.CostF} | g={startNode.CostG}");

        //others
        while (openList.Count > 0)
        {
            TNode currentNode = GetLowestFCostNode(openList);

            Debug.LogError($"Check: {currentNode.Position.LocString}");

            if (currentNode.Position == endNode.Position)
            {
                //reached final
                Debug.Log("Done!");
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neughbour in grid.GetAllNeighbours(currentNode))
            {
                if (closedList.Contains(neughbour))
                    continue;

                if (neughbour.Blocked)                  
                {
                    closedList.Add(neughbour);
                    continue;
                }

                Debug.LogWarning($"{neughbour.Position.LocString} === h={neughbour.CostH} | f={neughbour.CostF}");

                int provisionalGCost = currentNode.CostG + CalculateDistanceCost(currentNode, neughbour);

                if (provisionalGCost < neughbour.CostG)
                {
                    neughbour.CameFrom = currentNode;
                    neughbour.CostG = provisionalGCost;
                    neughbour.CostH = CalculateDistanceCost(neughbour, endNode);

                    if (!openList.Contains(neughbour))
                        openList.Add(neughbour);
                }
            }
        }

        //Out of nodes
        Debug.LogError("No path");
        return null;
    }

    private List<TNode> CalculatePath(TNode endnode)
    {
        List<TNode> path = new List<TNode>();

        path.Add(endnode);

        TNode currentNode = endnode;

        while (currentNode.CameFrom != null)
        {
            path.Add(currentNode);
            currentNode = (TNode)currentNode.CameFrom;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(TNode a, TNode b)
    {
        int xDist = (int)MathF.Abs(a.Position.X - b.Position.X);
        int yDist = (int)MathF.Abs(a.Position.Y - b.Position.Y);
        int remaining = (int)MathF.Abs(xDist - yDist);

        int dist = COST_DIAGONAL * Mathf.Min(xDist, yDist) + COST_STRAIGHT * remaining;

        Debug.Log($"{a.Position.LocString} --> {b.Position.LocString}| Dist={dist}");

        return dist;
    }

    private TNode GetLowestFCostNode(List<TNode> nodes)
    {
        var lowest = nodes[0];

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].CostF < lowest.CostF)
            {
                lowest = nodes[i];
            }
        }

        return lowest;
    }
}
