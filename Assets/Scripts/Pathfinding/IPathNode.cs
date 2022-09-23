namespace Pathfinding
{
    public interface IPathNode
    {
        public GridPosition Position { get; set; }
        public IPathNode CameFrom { get; set; } //Move to another interface?
        public bool Blocked { get; }
        public int CostG { get; set; }
        public int CostH { get; set; }
        public int CostF { get; }
    }
}
