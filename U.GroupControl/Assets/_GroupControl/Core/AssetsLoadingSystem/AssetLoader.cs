using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GroupControl.Core.AssetsLoadingSystem
{
    public class AssetsLoader : MonoBehaviour, IMapLoader, ICharacterLoader
    {
        public async Task<GameObject> LoadMapAsync(IAssetsConfig config)
        {
            GameObject mapPrefab = await config.MapPrefabReference.LoadAssetAsync<GameObject>().Task;
            GameObject map = Instantiate(mapPrefab);
            
            return map;
        }

        public async Task<GameObject[]> LoadCharactersAsync(IAssetsConfig config, int characterCount = 1)
        {
            try
                {
                    GameObject characterPrefab = await config.CharacterPrefabReference.LoadAssetAsync<GameObject>().Task;
                    GameObject[] characters = new GameObject[characterCount];
                    for (int i = 0; i < characterCount; i++)
                    {
                        Vector3 randomPosition = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
                        characters[i] = Instantiate(characterPrefab, randomPosition, Quaternion.identity);
                    }
            
                    config.CharacterPrefabReference.ReleaseAsset();
                    return characters;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error loading characters: {e.Message}\nStack trace: {e.StackTrace}");
                    throw; // Rethrow the exception after logging for further analysis.
                }
        }
    }
}
