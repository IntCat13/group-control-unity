using _GroupControl.Core.Characters.Types;
using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;

namespace _GroupControl.Core.Characters.CharacterUtils
{
    public static class CharacterMovement
    {
        public static NavigationInfo MoveToTarget(this NavigationInfo navigationInfo, Transform transform, CharacterInfo info)
        {
            if (navigationInfo.IndexPath >= navigationInfo.TargetPath.Length)
                return new NavigationInfo(null);

            RotateToTarget(transform, navigationInfo.TargetPath[navigationInfo.IndexPath], info);

            float distanceToTheNextWayPoint = Vector3.Distance(transform.position, navigationInfo.TargetPath[navigationInfo.IndexPath]);
            float distanceToFinaltWayPoint = Vector3.Distance(transform.position, navigationInfo.TargetPath[navigationInfo.TargetPath.Length - 1]);

            if (distanceToTheNextWayPoint < info.StoppingDistance)
                navigationInfo.IndexPath++;

            if (distanceToFinaltWayPoint < info.StoppingDistance)
                navigationInfo.IndexPath = navigationInfo.TargetPath.Length;
            
            else if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(navigationInfo.TargetPath[navigationInfo.IndexPath] - transform.position)) < info.RotationCompleteThreshold)
                transform.position = Vector3.MoveTowards(transform.position, navigationInfo.TargetPath[navigationInfo.IndexPath], info.Speed * Time.deltaTime);
            
            return navigationInfo;
        }
        
        public static bool IsDistanceReached (this float minDistance, Vector3 position, Vector3 target)
            => Vector3.Distance(position, target) < minDistance;

        private static void RotateToTarget(Transform transform, Vector3 target, CharacterInfo info)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, info.RotationSpeed * Time.deltaTime);
        }
        
        private static bool IsPointReached(float distance, float minDistance)
            => distance < minDistance;
        
        private static bool IsRotationComplete(Vector3 target, Transform transform, float rotationCompleteThreshold)
            => Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target - transform.position)) < rotationCompleteThreshold;
    }
}