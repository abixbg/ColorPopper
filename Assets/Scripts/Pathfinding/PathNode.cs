using UnityEngine;
using Unity.Mathematics;


namespace Pathfinding
{
    [System.Serializable]
    public class PathNode
    {

        private PathfindingGrid grid;
        [SerializeField] private GridPosition location;
        [SerializeField] private bool walkable;

        public int gCost;
        public int hCost;
        public int fCost;
        

        public PathNode CameFrom;

        public GridPosition Location { get => location;}
        public string LocString => $"[{location.X},{location.Y}]";

        public bool HighLight { get; set; }
        public bool Walkable => walkable;

        public PathNode(GridPosition location)
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
