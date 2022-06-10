using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public struct CellData
{
    [SerializeField] private float2 coordinates;

    public float2 Coordiantes { get => coordinates; }

    public CellData(float2 coordinates)
    {
        this.coordinates = coordinates;
    }
}
