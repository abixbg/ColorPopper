using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
    public class PathfindingGrid<TNode> where TNode : IPathNode, new()
    {
        private readonly int2 _size;
        private readonly List<TNode> nodes;
        public List<TNode> Nodes => nodes;

        public PathfindingGrid(int2 size)
        {
            _size = size;
            int[,] locArray = new int[size.x, size.y];

            nodes = new List<TNode>();

            for (int x = 0; x < locArray.GetLength(0); x++)
            {
                for (int y = 0; y < locArray.GetLength(1); y++)
                {
                    var node = new TNode();
                    node.Position = new GridPosition(x, y);
                    nodes.Add(node);
                }
            }
            Debug.Log($"Size =({size.x}, {size.y}) | coutn = {nodes.Count}");
        }

        public TNode GetNodeAt(GridPosition location)
        {
            return nodes.Find(n => GridPosition.Equals(n.Position, location));
        }

        public List<TNode> GetAllNeighbours(PathNode currentNode)
        {
            var neighbours = new List<TNode>();

            Debug.LogError($"GRID --> {_size}");

            if (currentNode.Location.X - 1 >= 0)
            {
                //SW
                if (currentNode.Location.Y - 1 >= 0)
                    neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X - 1, currentNode.Location.Y - 1)));

                //W
                neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X - 1, currentNode.Location.Y)));

                //NW
                if (currentNode.Location.Y + 1 >= 0)
                    neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X - 1, currentNode.Location.Y + 1)));
            }

            //S
            if (currentNode.Location.Y - 1 >= 0)
                neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X, currentNode.Location.Y - 1)));

            //N
            if (currentNode.Location.Y + 1 >= 0)
                neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X, currentNode.Location.Y + 1)));

            if (currentNode.Location.X + 1 < _size.x)
            {
                //SE
                if (currentNode.Location.Y - 1 >= 0)
                    neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X + 1, currentNode.Location.Y - 1)));

                //E
                neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X + 1, currentNode.Location.Y)));

                //NE
                if (currentNode.Location.Y + 1 < _size.y)
                    neighbours.Add(GetNodeAt(new GridPosition(currentNode.Location.X + 1, currentNode.Location.Y + 1)));
            }
            return neighbours;
        }
    }
}

