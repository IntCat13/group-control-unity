using System.Threading.Tasks;
using UnityEngine;

namespace _GroupControl.Core.AssetsLoadingSystem
{
    public interface IMapLoader
    {
        public Task<GameObject> LoadMapAsync(IAssetsConfig config);
    }
}