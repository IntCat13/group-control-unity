using _GroupControl.Core.NavigationSystem.Types;

namespace _GroupControl.Core.NavigationSystem.Utilities
{
    public static class NeighbourNodeUtils
    {
        public static void UpdateNeighbourNode(Node currentNode, Node neighbourNode, Node targetNode, Heap<Node> openList)
        {
            float gCost = currentNode.gCost + MovementCostCalculator.CalculateMovementCost(currentNode, neighbourNode);
            float hCost = MovementCostCalculator.CalculateMovementCost(neighbourNode, targetNode);

            if (!openList.Contains(neighbourNode))
                AddNewNeighbourNode(currentNode, neighbourNode, hCost + gCost, openList);
            else
                UpdateExistingNeighbourNode(currentNode, neighbourNode, gCost + hCost);
        }

        private static void AddNewNeighbourNode(Node currentNode, Node neighbourNode, float fCost, Heap<Node> openList)
        {
            neighbourNode.fCost = fCost;
            neighbourNode.parent = currentNode;
            openList.Add(neighbourNode);
        }

        private static void UpdateExistingNeighbourNode(Node currentNode, Node neighbourNode, float newFCost)
        {
            if (neighbourNode.fCost > newFCost)
            {
                neighbourNode.fCost = newFCost;
                neighbourNode.parent = currentNode;
            }
        }
    }
}