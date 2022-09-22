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
                    var node = new PathNode(new int2(x,y));
                    nodes.Add(node);
                }
            }



            Debug.Log($"Size =({_size.x}, {_size.y}) | coutn = {nodes.Count}");
        }

        public PathNode GetNodeAt(int2 location)
        {
            return nodes.Find(n => n.Location.x == location.x && n.Location.y == location.y);
        }
    }
}

