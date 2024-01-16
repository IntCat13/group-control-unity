using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;

namespace _GroupControl.Core.GameManagement.SavingSystem
{
    public struct CharacterSaveData
    {
        public Vector3 Position;
        public CharacterInfo CharacterInfo;
        
        public CharacterSaveData(Vector3 position, CharacterInfo characterInfo)
        {
            Position = position;
            CharacterInfo = characterInfo;
        }
    }
}