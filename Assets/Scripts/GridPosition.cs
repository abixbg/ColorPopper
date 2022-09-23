using System;
using System.Drawing;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

namespace Pathfinding
{
    [System.Serializable]
    public struct GridPosition
    {
        [SerializeField] public int x;
        [SerializeField] public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public string LocString => $"[{x},{y}]";

        public override bool Equals(object obj)
        {
            return obj is GridPosition position &&
                   x == position.x &&
                   y == position.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return a.x != b.x || a.y != b.y;
        }
    }
}
