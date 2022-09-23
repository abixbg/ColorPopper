using AGK.GameGrids.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class InspectGrid : MonoBehaviour
{
    [SerializeField] private List<NodeVisual> nodeVisuals;
    [SerializeField] private List<PathNode> nodes;

    [SerializeField] private PrototypeGrid _prototype;

    [SerializeField] private float cellSize;
    [SerializeField] private NodeVisual nodePrefab;

    private PathfindingGrid<PathNode> Grid => _prototype.Grid;

    private bool isInit;

    private void Update()
    {
        if (!isInit && _prototype.Grid != null)
            Init();

        if (isInit)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        foreach (var node in nodeVisuals)
        {
            node.UpdateData(Grid.GetNodeAt(node.LocationData));
        }
    }

    private void Init()
    {
        nodes = Grid.Nodes;

        foreach (var node in Grid.Nodes)
        {
            var nodeObj = Instantiate(nodePrefab, transform);

            nodeObj.transform.localPosition = new Vector3(node.Location.X, node.Location.Y, 0) * cellSize;
            nodeObj.Cosntruct(node.Location);
            nodeVisuals.Add(nodeObj);
        }
        isInit = true;
    }

    public void CommandHighlight()
    {
        var path = _prototype.Path;

        foreach (var node in path)
        {
            node.HighLight = true;
        }
    }
}
