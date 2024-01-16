using System.Collections.Generic;
using _GroupControl.Core.NavigationSystem.Types;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem.Utilities
{
    public static class PathfindingUtils
    {
        public static PathResponse SearchPath(Node targetNode, Heap<Node> openList, List<Node> closeList, NavigationGrid grid)
        {
            while (openList.Count > 0)
            {
                Node currentNode = openList.Pop();

                if (currentNode == null)
                    return new PathResponse(null, false, null);

                if (grid.CheckIfPointsLinkedTogether(currentNode, targetNode))
                {
                    targetNode.parent = currentNode;
                    return new PathResponse(null, true, null);
                }

                closeList.Add(currentNode);

                List<Node> neighboursNode = currentNode.neighbors;

                foreach (Node neighbourNode in neighboursNode)
                {
                    if (closeList.Contains(neighbourNode))
                        continue;

                    NeighbourNodeUtils.UpdateNeighbourNode(currentNode, neighbourNode, targetNode, openList);
                }
            }

            return new PathResponse(null, false, null);
        }

        public static Vector3[] CreatePath(Node agentNode, Node targetNode)
        {
            List<Node> pathNodes = new List<Node>();
            Node currentNode = targetNode;

            while (currentNode != agentNode)
            {
                pathNodes.Add(currentNode);
                currentNode = currentNode.parent;
            }

            pathNodes.Reverse();

            Vector3[] path = new Vector3[pathNodes.Count];

            for (int i = 0; i < pathNodes.Count; i++)
                path[i] = pathNodes[i].position;

            return path;
        }
    }
}