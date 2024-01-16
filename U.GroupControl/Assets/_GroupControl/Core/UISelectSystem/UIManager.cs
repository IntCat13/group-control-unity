using _GroupControl.Core.GameManagement.CharactersManagement;
using UnityEngine;

namespace GroupControl._GroupControl.Core.UISelectSystem
{
    public class UIManager: MonoBehaviour
    {
        public void SelectCharacter(int characterIndex)
        {
            CharactersManager.Selector.SelectCharacter(characterIndex); 
        }
    }
}