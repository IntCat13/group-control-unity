using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;

namespace _GroupControl.Core.Characters
{
    public interface ICharacter
    {
        void Initialize(CharacterInfo info, Vector3 startPosition = default);
        void SetDestination(Vector3 destination);
        void SetLeader(Transform leader);
        CharacterInfo GetCharacterInfo();
        Transform CurrentPoint { get; }
    }
}