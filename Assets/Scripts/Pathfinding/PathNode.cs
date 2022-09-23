using UnityEngine;
using Unity.Mathematics;


namespace Pathfinding
{
    [System.Serializable]
    public class PathNode
    {
        [SerializeField] private GridPosition location;
        [SerializeField] private bool blocked;

        public int gCost;
        public int hCost;
        public int fCost;
        
        public PathNode CameFrom;

        public GridPosition Location { get => location;}
        public string LocString => $"[{location.X},{location.Y}]";

        public bool HighLight { get; set; }
        public bool Blocked => blocked;

        public PathNode(GridPosition location)
        {
            this.location = location;
            this.blocked = false;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public void SetBlocked()
        {
            blocked = true;
        }
    }
}
