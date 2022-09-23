using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
    public class PathfindingGrid
    {
        private readonly int2 _size;
        private readonly List<PathNode> nodes;

        public List<PathNode> Nodes => nodes;
        public int2 Size => _size;

        public PathfindingGrid(int2 size)
        {
            _size = size;

            int[,] locArray = new int[size.x, size.y];

            nodes = new List<PathNode>();

            for (int x = 0; x < locArray.GetLength(0); x++)
            {
                for (int y = 0; y < locArray.GetLength(1); y++)
                {
                    var node = new PathNode(new GridPosition(x,y));
                    nodes.Add(node);
                }
            }
            Debug.Log($"Size =({_size.x}, {_size.y}) | coutn = {nodes.Count}");
        }

        public PathNode GetNodeAt(GridPosition location)
        {
            return nodes.Find(n => GridPosition.Equals(n.Location, location));
        }

        public List<PathNode> GetAllNeighbours(PathNode currentNode)
        {
            var neighbours = new List<PathNode>();

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

