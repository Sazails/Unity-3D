using UnityEngine;

namespace Assets._Core.Scripts._Pathfinding
{
    public struct Node
    {
        public Vector2Int _position;
        public Vector2Int _parentPosition;
        public Vector2Int[] _neighbours;

        public float _costToParent;
        public float _costToEnd;
        public float _totalCost;
    }
}