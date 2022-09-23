using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class NodeVisual : MonoBehaviour
{
    [SerializeField] private TextMesh gText;
    [SerializeField] private TextMesh fText;
    [SerializeField] private TextMesh hText;
    [SerializeField] private TextMesh coordinates;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer highlight;

    private GridPosition locationData;
    public GridPosition LocationData => locationData;
 
    public void Cosntruct(GridPosition location)
    {
        locationData = location;
    }

    public void UpdateData(PathNode node)
    {
        gText.text = node.gCost.ToString();

        if (node.fCost > 2000)
            fText.text = "inf";
        else
            fText.text = node.fCost.ToString();
        
        hText.text = node.hCost.ToString();

        coordinates.text = $"({node.Location.X},{node.Location.Y} )";

        background.color = ((IPathNode)node).Blocked ? Color.red : Color.grey;
        highlight.color = node.HighLight ? Color.green : Color.grey;
    }
}
