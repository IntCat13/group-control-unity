﻿using _GroupControl.Core.AssetsLoadingSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _GroupControl.Core.GameManagement
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configurations/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject, IAssetsConfig
    {
        [Range(1,3)]
        public int characterCount = 5;
        public AssetReferenceGameObject mapPrefabReference;
        public AssetReferenceGameObject characterPrefabReference;

        public AssetReferenceGameObject MapPrefabReference => mapPrefabReference;
        public AssetReferenceGameObject CharacterPrefabReference => characterPrefabReference;
    }
}