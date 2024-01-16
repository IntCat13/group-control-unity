using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _GroupControl.Core.AssetsLoadingSystem;
using _GroupControl.Core.Characters;
using _GroupControl.Core.Characters.Types;
using _GroupControl.Core.GameManagement.SavingSystem;
using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;


namespace _GroupControl.Core.GameManagement
{
    public class CharactersManager: MonoBehaviour, ICharactersManager
    {
        private ISaveManager _saveManager;
        private ICharacterLoader _characterLoader;
        private ICharacter[] _characters;
        private CharacterConfig _characterConfig;
        private int _currentCharacterIndex;
        private GameConfig _gameConfig;

        public async void Initialize(ICharacterLoader characterLoader, GameConfig gameConfig, CharacterConfig characterConfig, ISaveManager saveManager)
        {
            _saveManager = saveManager;
            _characterConfig = characterConfig;
            _gameConfig = gameConfig;
            _characterLoader = characterLoader;
            await LoadCharactersAssetsAsync();
        }
        
        public void MoveCharacter(Vector3 targetPosition)
        {
            _characters[_currentCharacterIndex].SetDestination(targetPosition);
        }
        
        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentCharacterIndex++;
                if (_currentCharacterIndex >= _characters.Length)
                    _currentCharacterIndex = 0;
                
                InitLeaders();
            }
            
            if (Input.GetKeyDown(KeyCode.F5))
                await Save();
            
            if (Input.GetKeyDown(KeyCode.F6))
                Load();
        }

        private Task Save()
        {
            var saveData = new List<CharacterSaveData>();
            foreach (var character in _characters)
                saveData.Add(new CharacterSaveData( character.CurrentPoint.position, character.GetCharacterInfo()));
            
            return _saveManager.SaveCharactersAsync(saveData , Application.persistentDataPath + "/save.json");
        }
        
        private void Load()
        {
            ClearCharacters();
            var savedData = _saveManager.LoadCharactersAsync(Application.persistentDataPath + "/save.json");
            LoadCharactersAssetsAsync(savedData);
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
        
        private async Task LoadCharactersAssetsAsync(CharacterSaveData[] charactersData = null)
        {
            var _charactersObjects = await _characterLoader.LoadCharactersAsync(_gameConfig, _gameConfig.characterCount);
            _characters = new ICharacter[_charactersObjects.Length];
            for (int i = 0; i < _charactersObjects.Length; i++)
            {
                _characters[i] = _charactersObjects[i].GetComponent<Character>();
                
                if (charactersData == null)
                    _characters[i].Initialize(new CharacterInfo(_characterConfig));
                else
                    _characters[i].Initialize(charactersData[i].CharacterInfo, charactersData[i].Position);
            }
            InitLeaders();
        }

        private void ClearCharacters()
        {
            var characters = GameObject.FindGameObjectsWithTag("character");
            foreach (var character in characters)
                Destroy(character);
            
            _characters = null;
        }
    }
}