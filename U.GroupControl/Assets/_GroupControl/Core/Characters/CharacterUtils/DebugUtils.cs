using _GroupControl.Core.Characters.Types;
using UnityEngine;

namespace _GroupControl.Core.Characters.CharacterUtils
{
    public static class DebugUtils
    {
        public static void DrawPath(this NavigationInfo info, Color pathColor)
        {
            Gizmos.color = pathColor;
            if (info.TargetPath != null && info.TargetPath.Length > 0)
                for (int i = 0; i < info.TargetPath.Length - 1; i++)
                    Gizmos.DrawLine(info.TargetPath[i], info.TargetPath[i + 1]);
            
        }
    }
}