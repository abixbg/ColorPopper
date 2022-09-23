using UnityEngine;

namespace Pathfinding
{
    [System.Serializable]
    public class PathNode : IPathNode
    {
        [SerializeField] private GridPosition location;
        [SerializeField] private bool blocked;
        [SerializeField] private IPathNode cameFrom;

        public int gCost;
        public int hCost;
        public int fCost;
        
        public GridPosition Location { get => location;}
        GridPosition IPathNode.Position { get => location; set => location = value; }

        public bool HighLight { get; set; }

        bool IPathNode.Blocked => blocked;
        IPathNode IPathNode.CameFrom { get => cameFrom; set => cameFrom = value; }
        int IPathNode.CostG { get => gCost; set => gCost = value; }
        int IPathNode.CostH { get => hCost; set => gCost = value; }
        int IPathNode.CostF { get => gCost + hCost; }

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
