using UnityEngine.AddressableAssets;

namespace _GroupControl.Core.AssetsLoadingSystem
{
    public interface IAssetsConfig
    {
        public AssetReferenceGameObject MapPrefabReference { get; }
        public AssetReferenceGameObject CharacterPrefabReference { get; }
    }
}
