
using UnityEngine;

namespace _GroupControl.Core.Characters.Types
{
    public struct NavigationInfo
    {
        public Vector3[] TargetPath { get; private set; }
        public int IndexPath { get; set; }
        
        public NavigationInfo(Vector3[] targetPath)
        {
            TargetPath = targetPath;
            IndexPath = 0;
        }
    }
}