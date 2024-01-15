using UnityEngine;

namespace _GroupControl.Core.Characters.Types
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configurations/CharacterConfig", order = 1)]
    public class CharacterConfig : ScriptableObject
    {
        [Header("Speed Settings")]
        [Range(1, 10)]
        public float MinSpeed = 5f;
        [Range(1, 10)]
        public float MaxSpeed = 10f;
        
        [Space]
        [Header("Rotation Settings")]
        [Range(1, 10)]
        public float MinRotationSpeed = 5f;
        [Range(1, 10)]
        public float MaxRotationSpeed = 10f;
        public float RotationCompleteThreshold = 0.1f;
        
        [Space]
        [Header("Other")]
        public float StoppingDistance = 0.1f;
        public float LeaderStoppingDistance = 0.5f;
        public float AvarageStamina = 20f;
    }
}