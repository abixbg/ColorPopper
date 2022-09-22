using Pathfinding;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InspectGrid : MonoBehaviour
{
    [SerializeField] private int2 gridSize;
    [SerializeField] private List<NodeVisual> nodeVisuals;
    [SerializeField] private List<PathNode> nodes;

    [SerializeField] private PrototypeGrid _prototype;

    [SerializeField] private float cellSize;
    [SerializeField] private NodeVisual nodePrefab;

    private PathfindingGrid Grid => _prototype.Grid;

    private bool isInit;

    private void Update()
    {
        if (!isInit && _prototype.Grid!=null)
            Init();

        if (isInit)
        {
            UpdateUI();
        }        
    }

    private void UpdateUI()
    {
        gridSize = Grid.Size;
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

            nodeObj.transform.localPosition = new Vector3(node.Location.x, node.Location.y, 0) * cellSize;
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
