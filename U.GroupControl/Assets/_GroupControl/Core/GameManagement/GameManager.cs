using System.Threading.Tasks;
using _GroupControl.Core.AssetsLoadingSystem;
using _GroupControl.Core.Characters.Types;
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
            await InitializeGame();
            await LoadMapAssetsAsync();
        }

        private Task InitializeGame()
        {
            var assetLoader = gameObject.AddComponent<AssetsLoader>();
            var charactersManager = gameObject.AddComponent<CharactersManager>();
            var userInput = gameObject.AddComponent<UserInputManager>();
            
            charactersManager.Initialize(assetLoader, gameConfig, characterConfig);
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