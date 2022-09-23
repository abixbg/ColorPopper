using UnityEngine;

namespace Pathfinding
{
    [System.Serializable]
    public class PathNode : IPathNode
    {
        [SerializeField] private GridPosition location;
        [SerializeField] private bool blocked;
        [SerializeField] private PathNode cameFrom;

        public int gCost;
        public int hCost;
        public int fCost;
        
        public PathNode CameFrom;

        public GridPosition Location { get => location;}
        GridPosition IPathNode.Position { get => location; set => location = value; }
        public string LocString => $"[{location.X},{location.Y}]";

        public bool HighLight { get; set; }
        public bool Blocked => blocked;

        public PathNode()
        {
            blocked = false;
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
