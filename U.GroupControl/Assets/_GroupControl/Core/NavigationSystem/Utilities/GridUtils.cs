using System;
using System.Collections.Generic;
using _GroupControl.Core.NavigationSystem.Types;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem.Utilities
{
    public static class GridUtils
    {
        public static List<Node> BuildPointNode(this List<Node> grid, Transform obstacle, Func<Vector3, bool> checkIsWalkable, Func<Node, List<Node>> calculateNeighbors)
        {
            float xSize = obstacle.localScale.x / 2 + 0.35f;
            float zSize = obstacle.localScale.z / 2 + 0.35f;

            Vector3 forwardDirection = obstacle.forward * zSize;
            Vector3 rightDirection = obstacle.right * xSize;

            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    Vector3 pos = i * forwardDirection + j * rightDirection + obstacle.position;
                    if (checkIsWalkable(pos))
                        grid.Add(new Node(pos, calculateNeighbors(new Node(pos))));
                }
            }
            
            return grid;
        }

        public static void RemoveAllNonWalkableNodes(List<Node> grid, Func<Vector3, bool> checkIsWalkable)
        {
            grid.RemoveAll(node => !checkIsWalkable(node.position));
        }

        public static List<Node> CalculateNeighbors(List<Node> grid, Node pointNode, Func<Node, Node, bool> checkIfPointsLinkedTogether)
        {
            List<Node> neighbors = new List<Node>();

            foreach (Node node in grid)
                if (checkIfPointsLinkedTogether(pointNode, node) && pointNode.position != node.position)
                    neighbors.Add(node);

            return neighbors;
        }
    }
}