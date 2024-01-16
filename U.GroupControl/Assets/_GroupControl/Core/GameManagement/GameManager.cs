using System.Threading.Tasks;
using _GroupControl.Core.AssetsLoadingSystem;
using _GroupControl.Core.Characters.Types;
using _GroupControl.Core.GameManagement.CharactersManagement;
using _GroupControl.Core.GameManagement.SavingSystem;
using UnityEngine;

namespace _GroupControl.Core.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private CharacterConfig characterConfig;

        private IMapLoader _mapLoader;
        private ICharactersManager _charactersManager;

        private async void Awake()
        {
            await InitializeDependencies();
            await LoadMapAssetsAsync();
        }

        private Task InitializeDependencies()
        {
            var assetLoader = gameObject.AddComponent<AssetsLoader>();
            var charactersManager = gameObject.AddComponent<CharactersManager>();
            var userInput = gameObject.AddComponent<UserInputManager>();
            var saveManager = gameObject.AddComponent<SaveManager>();
            
            charactersManager.Initialize(assetLoader, gameConfig, characterConfig, saveManager);
            userInput.Initialize(charactersManager);

            _mapLoader = assetLoader;
            return Task.CompletedTask;
        }

        private async Task LoadMapAssetsAsync()
        {
            await _mapLoader.LoadMapAsync(gameConfig);
        }
    }
}