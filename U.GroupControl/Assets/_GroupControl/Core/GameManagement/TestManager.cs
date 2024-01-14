using System.Threading.Tasks;
using _GroupControl.Core.AssetsLoadingSystem;
using UnityEngine;

namespace _GroupControl.Core.GameManagement
{
    public class TestManager : MonoBehaviour
    {
        [Header("Game Config")]
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private GameObject[] _characters;
        
        private IMapLoader _mapLoader;
        private ICharacterLoader _characterLoader;

        private async void Start()
        {
            Initialize();
            await LoadAssetsAsync();
        }

        private void Initialize()
        {
            var assetLoader = gameObject.AddComponent<AssetsLoader>();
            _mapLoader = assetLoader;
            _characterLoader = assetLoader;
        }

        private async Task LoadAssetsAsync()
        {
            await _mapLoader.LoadMapAsync(gameConfig);
            _characters = await _characterLoader.LoadCharactersAsync(gameConfig, gameConfig.characterCount);
        }
    }
}
