using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Navigation.Types
{
    public class Node: IComparable<Node> {

        public Vector3 position;
        public Node parent;
        public List<Node> neighbors = new List<Node>();
        public float fCost;
        public readonly float gCost;
        private float _hCost;
        
        public Node(Vector3 newPosition)
        {
            position = newPosition;
            gCost = 0;
            _hCost = 0;
        }
        
        public Node(Vector3 newPositions, List<Node> neighbors)
        {
            position = newPositions;
            this.neighbors = neighbors;
            gCost = 0;
            _hCost = 0;
            fCost = 0;
        }

        public void AddNeighbors(List<Node> newNeighbors)
        {
            neighbors = newNeighbors;
        }

        public int CompareTo(Node nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
                compare = _hCost.CompareTo(nodeToCompare._hCost);

            return -compare;
        }
    }
}
