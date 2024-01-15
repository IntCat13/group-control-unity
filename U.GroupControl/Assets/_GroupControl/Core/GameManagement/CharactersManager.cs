using System.Threading.Tasks;
using _GroupControl.Core.AssetsLoadingSystem;
using _GroupControl.Core.Characters;
using _GroupControl.Core.Characters.Types;
using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;


namespace _GroupControl.Core.GameManagement
{
    public class CharactersManager: MonoBehaviour, ICharactersManager
    {
        private ICharacter[] _characters;
        private CharacterConfig _characterConfig;
        private int _currentCharacterIndex;

        public async void Initialize(ICharacterLoader characterLoader, GameConfig gameConfig, CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            await LoadCharactersAssetsAsync(characterLoader, gameConfig);
        }
        
        public void MoveCharacter(Vector3 targetPosition)
        {
            _characters[_currentCharacterIndex].SetDestination(targetPosition);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentCharacterIndex++;
                if (_currentCharacterIndex >= _characters.Length)
                    _currentCharacterIndex = 0;
                
                InitLeaders();
            }
        }

        private void InitLeaders()
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                if (i == _currentCharacterIndex)
                    _characters[i].SetLeader(null);
                else
                    _characters[i].SetLeader(_characters[_currentCharacterIndex].CurrentPoint);
            }
        }
        
        private async Task LoadCharactersAssetsAsync(ICharacterLoader characterLoader, GameConfig gameConfig)
        {
            var _charactersObjects = await characterLoader.LoadCharactersAsync(gameConfig, gameConfig.characterCount);
            _characters = new ICharacter[_charactersObjects.Length];
            for (int i = 0; i < _charactersObjects.Length; i++)
            {
                _characters[i] = _charactersObjects[i].GetComponent<Character>();
                _characters[i].Initialize(new CharacterInfo(_characterConfig));
            }
            InitLeaders();
        }
    }
}