using System.Threading.Tasks;
using UnityEngine;

namespace _GroupControl.Core.AssetsLoadingSystem
{
    public interface ICharacterLoader
    {
        public Task<GameObject[]> LoadCharactersAsync(IAssetsConfig config, int characterCount = 1);
    }
}