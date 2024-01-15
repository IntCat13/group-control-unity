using UnityEngine;

namespace _GroupControl.Core.Characters.Types
{
    [System.Serializable]
    public struct CharacterInfo
    {
        [Header("Movement Settings")] 
        public float Speed;
        public float MaxStamina;
        public float Stamina;
        public float StoppingDistance;
        public float LeaderStoppingDistance;
        public float StaminaRecoveryTime;

        [Header("Rotation Settings")] 
        public float RotationSpeed;
        public float RotationCompleteThreshold;
        
        public CharacterInfo(CharacterConfig config)
        {
            Speed = Random.Range(config.MinSpeed, config.MaxSpeed);
            LeaderStoppingDistance = Random.Range(config.LeaderStoppingDistance/2, config.LeaderStoppingDistance*1.5f);
            StoppingDistance = config.StoppingDistance;
            RotationSpeed = Random.Range(config.MinRotationSpeed, config.MaxRotationSpeed);
            RotationCompleteThreshold = config.RotationCompleteThreshold;
            StaminaRecoveryTime = Random.Range(3f, 5f);
            
            var stamina = Random.Range(config.AvarageStamina/2, config.AvarageStamina*1.5f);
            MaxStamina = stamina;
            Stamina = stamina;
        }
    }
}