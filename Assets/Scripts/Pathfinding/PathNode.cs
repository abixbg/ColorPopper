using AGK.GameGrids;
using AGK.GameGrids.Pathfinding;
using UnityEngine;


[System.Serializable]
public class PathNode : IGridCell, IPathNode
{
    [SerializeField] private GridPosition location;
    [SerializeField] private bool blocked;
    [SerializeField] private IPathNode cameFrom;

    [Header("Islands")]
    [SerializeField] private bool isChecked;
    [SerializeField] private int somethingToMatch;
    [SerializeField] private int groupID;
    public int GroupID => groupID;

    public int gCost;
    public int hCost;
    public int fCost;

    public GridPosition Location { get => location; }
    GridPosition IGridCell.Position { get => location; set => location = value; }

    public bool HighLight { get; set; }
    public int SomeAttribute { get => somethingToMatch; set => somethingToMatch = value; }

    bool IPathNode.Blocked => blocked;
    IPathNode IPathNode.CameFrom { get => cameFrom; set => cameFrom = value; }
    int IPathNode.CostG { get => gCost; set => gCost = value; }
    int IPathNode.CostH { get => hCost; set => gCost = value; }
    int IPathNode.CostF { get => gCost + hCost; }

    public PathNode()
    {
        blocked = false;
        //group = new GridCellGroup<IGroupfindNode>(0);

    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetBlocked(bool state)
    {
        blocked = state;
    }
}

