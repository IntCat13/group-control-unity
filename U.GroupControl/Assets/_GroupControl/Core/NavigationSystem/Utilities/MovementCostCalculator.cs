using GameSystems.Navigation.Types;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem.Utilities
{
    public static class MovementCostCalculator
    {
        public static float CalculateMovementCost(Node node, Node targetNode)
            => Vector3.Distance(node.position, targetNode.position);
    }
}