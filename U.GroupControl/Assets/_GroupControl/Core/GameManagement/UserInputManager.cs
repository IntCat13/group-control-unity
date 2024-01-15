using UnityEngine;

namespace _GroupControl.Core.GameManagement
{
    public class UserInputManager: MonoBehaviour
    {
        private ICharactersManager _characterManager;
        private Camera _camera;
        
        public void Initialize(ICharactersManager characterManager)
        {
            _characterManager = characterManager;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                
                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("floor"))
                    {
                        Vector3 hitPoint = hit.point;
                        _characterManager.MoveCharacter(hitPoint);
                    }
                }
            }
        }
    }
}