using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;


namespace _GroupControl.Core.GameManagement.SavingSystem
{
    public class SaveManager : MonoBehaviour, ISaveManager
    {
        public async Task SaveCharactersAsync(List<CharacterSaveData> characterInfoList, string filePath)
        {
            string json = "[";

            for (int i = 0; i < characterInfoList.Count; i++)
            {
                json += JsonUtility.ToJson(characterInfoList[i], true);
                    
                if (i < characterInfoList.Count - 1)
                    json += ",";
            }

            json += "]";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(json);
                Debug.Log("Saved: " + filePath);
            }
        }
        
        public CharacterSaveData[] LoadCharactersAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();
                    
                    string[] characterInfoStrings = json.Split(new[] { "},{" }, StringSplitOptions.None);
                    
                    characterInfoStrings[0] = characterInfoStrings[0].TrimStart('[', '{');
                    characterInfoStrings[^1] = characterInfoStrings[^1].TrimEnd('}', ']');
                    
                    List<CharacterSaveData> characterSaveDataList = new List<CharacterSaveData>();
                    foreach (string characterInfoString in characterInfoStrings)
                    {
                        string formattedString = "{" + characterInfoString + "}";
                        CharacterSaveData characterSaveData = JsonUtility.FromJson<CharacterSaveData>(formattedString);
                        characterSaveDataList.Add(characterSaveData);
                    }

                    Debug.Log("Loaded: " + filePath);
                    return characterSaveDataList.ToArray();
                }
            }
            else
            {
                Debug.LogError("FileNotFound: " + filePath);
                return null;
            }
        }
    }
}
