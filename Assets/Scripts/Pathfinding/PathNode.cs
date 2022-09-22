using UnityEngine;
using Unity.Mathematics;


namespace Pathfinding
{
    [System.Serializable]
    public class PathNode
    {

        private PathfindingGrid grid;
        [SerializeField] private int2 location;
        [SerializeField] private bool walkable;

        public int gCost;
        public int hCost;
        public int fCost;
        

        public PathNode CameFrom;

        public int2 Location { get => location;}
        public string LocString => $"[{location.x},{location.y}]";

        public bool HighLight { get; set; }
        public bool Walkable => walkable;

        public PathNode(int2 location)
        {
            this.location = location;
            this.walkable = true;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public void SetUnwalkable()
        {
            walkable = false;
        }
    }
}
