using UnityEngine;

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
    }
}
