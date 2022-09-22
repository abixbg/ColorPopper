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

    private int2 locationData;
    public int2 LocationData => locationData;
 
    public void Cosntruct(int2 location)
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

        coordinates.text = $"({node.Location.x},{node.Location.y} )";

        background.color = node.Walkable ? Color.grey : Color.red;
        highlight.color = node.HighLight ? Color.green : Color.grey;
    }
}
