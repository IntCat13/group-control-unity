using System;
using UnityEngine;

namespace _GroupControl.Core.AssetsLoadingSystem
{
    public interface ICharacterAssetLoader
    {
        void LoadAsset(string assetAddress, Action<GameObject> callback);
    }
}
