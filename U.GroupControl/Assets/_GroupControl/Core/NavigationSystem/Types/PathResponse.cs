using System;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem.Types
{
    public struct PathResponse
    {
        public Vector3[] path;
        public readonly bool success;
        public Action<Vector3[], bool> callBack;

        public PathResponse(Vector3[] path, bool success, Action<Vector3[], bool> callBack)
        {
            this.path = path;
            this.success = success;
            this.callBack = callBack;
        }

    }
}