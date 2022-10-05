using AGK.GameGrids;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public struct CellData
{
    [SerializeField] private GridPosition gridPos;
    [SerializeField] private float3 visualPos;

    public GridPosition Position { get => gridPos; set => gridPos = value; }
    public float3 VisualPosition { get => visualPos; set => visualPos = value; }

    public CellData(GridPosition gridPos, float3 visualPos)
    {
        this.gridPos = gridPos;
        this.visualPos = visualPos;
    }
}
