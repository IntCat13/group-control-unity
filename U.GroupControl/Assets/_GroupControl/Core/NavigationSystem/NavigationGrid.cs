using System.Collections.Generic;
using _GroupControl.Core.NavigationSystem.Utilities;
using GameSystems.Navigation.Types;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem
{
    public class NavigationGrid : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private Vector3 _worldPosition;
        [SerializeField] private Vector3 _worldSize;
        [SerializeField] private LayerMask _unWalkableLayers;
        private float _walkableRadius = 0.2f;
        
        private List<Node> grid = new List<Node>();
        public List<Node> Grid
        {
            get { return grid; }
            private set { grid = value; }
        }

        private float biggerBorder_X;
        private float smallerBorder_X;
        private float biggerBorder_Z;
        private float smallerBorder_Z;

        public bool CheckIfPointsLinkedTogether(Node point1, Node point2)
            => point1 != null && point2 != null && !Physics.Linecast(point1.position, point2.position, _unWalkableLayers);
        
        public Node GetPointNodeFromGridByPosition(Vector3 positions)
        {
            Node pointNode = new Node(positions);

            if(CheckIsWalkable(positions))
            {
                pointNode.neighbors = CalculateNeighbors(pointNode);
                return pointNode;        
            }

            return null;
        }
        
        private void Start()
        {
            CalculateWorldMapBorders();
            GenerateGrid();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            foreach (Node node in grid)
                Gizmos.DrawSphere(node.position, 0.1f);

            DrawGridBorders();
        }

        private void CalculateWorldMapBorders()
        {
            biggerBorder_X = _worldPosition.x + _worldSize.x / 2;
            smallerBorder_X = _worldPosition.x - _worldSize.x / 2;
            biggerBorder_Z = _worldPosition.z + _worldSize.z / 2;
            smallerBorder_Z = _worldPosition.z - _worldSize.z / 2;
        }

        private void DrawGridBorders()
        {
            Vector3[] corners = {
                new Vector3(smallerBorder_X, 0, smallerBorder_Z),
                new Vector3(biggerBorder_X, 0, smallerBorder_Z),
                new Vector3(biggerBorder_X, 0, biggerBorder_Z),
                new Vector3(smallerBorder_X, 0, biggerBorder_Z),
                new Vector3(smallerBorder_X, 0, smallerBorder_Z)
            };

            for (int i = 0; i < corners.Length - 1; i++)
                Gizmos.DrawLine(corners[i], corners[i + 1]);
        }

        private void GenerateGrid()
        {
            grid = new List<Node>();
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
                if (obj.layer == 8)
                    grid.BuildPointNode(obj.transform, CheckIsWalkable, CalculateNeighbors);
        }

        private bool CheckIsWalkable(Vector3 pointNodePosition)
        {
            if (pointNodePosition.x > biggerBorder_X || pointNodePosition.x < smallerBorder_X)
                return false;

            if (pointNodePosition.z > biggerBorder_Z || pointNodePosition.z < smallerBorder_Z)
                return false;
            
            if (Physics.OverlapSphere(pointNodePosition, _walkableRadius, _unWalkableLayers).Length > 0)
                return false;

            return true;
        }

        private List<Node> CalculateNeighbors(Node pointNode)
            => GridUtils.CalculateNeighbors(grid, pointNode, CheckIfPointsLinkedTogether);
    }
}