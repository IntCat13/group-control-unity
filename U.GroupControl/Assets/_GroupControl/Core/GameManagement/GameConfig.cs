using _GroupControl.Core.AssetsLoadingSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _GroupControl.Core.GameManagement
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configurations/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject, IAssetsConfig
    {
        [Header("Characters settings")]
        public int characterCount = 5;
        public AssetReferenceGameObject characterPrefabReference;
        [Space]
        [Header("Map settings")]
        public AssetReferenceGameObject mapPrefabReference;

        public AssetReferenceGameObject MapPrefabReference => mapPrefabReference;
        public AssetReferenceGameObject CharacterPrefabReference => characterPrefabReference;
    }
}