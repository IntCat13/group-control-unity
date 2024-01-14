using System;
using UnityEngine;

namespace GameSystems.Navigation.Types
{
    public struct PathRequest
    {
        public Vector3 startNode;
        public Vector3 targetNode;
        public readonly Action<Vector3[], bool> callBack;

        public PathRequest(Vector3 startNode, Vector3 targetNode, Action<Vector3[], bool> callBack)
        {
            this.startNode = startNode;
            this.targetNode = targetNode;
            this.callBack = callBack;
        }

    }
}