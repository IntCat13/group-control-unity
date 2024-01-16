using System.Collections.Generic;
using System.Threading.Tasks;
using _GroupControl.Core.Characters.Types;


namespace _GroupControl.Core.GameManagement.SavingSystem
{
    public interface ISaveManager
    {
        public Task SaveCharactersAsync(List<CharacterSaveData> characterInfoList, string filePath);
        public CharacterSaveData[] LoadCharactersAsync(string filePath);
    }
}