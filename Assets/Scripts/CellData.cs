using AGK.GameGrids;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public struct CellData : IGridCell
{
    [SerializeField] private GridPosition gridPos;
    [SerializeField] private float3 visualPos;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isActive;

    public GridPosition Position { get => gridPos; set => gridPos = value; }
    public float3 VisualPosition { get => visualPos; set => visualPos = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public bool IsActive { get => isActive; set => isActive = value; }

    public CellData(GridPosition gridPos, float3 visualPos)
    {
        this.gridPos = gridPos;
        this.visualPos = visualPos;
        isLocked = false;
        isActive = true;
    }
}
