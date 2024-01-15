using System.Collections.Generic;
using System.Threading;
using _GroupControl.Core.NavigationSystem.Types;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem
{
    public class PathRequestService : MonoBehaviour
    {
        private readonly Queue<PathResponse> _results = new Queue<PathResponse>();
        public static PathRequestService Instance { get; private set; }

        public void Request(PathRequest pathRequest)
        {
            ThreadStart threadStart = delegate { PathfindingManager.Instance.FindPath(pathRequest, FinishProcessing); };
            threadStart.Invoke();
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        private void Update()
        {
            CallBackTheResult();
        }

        private void CallBackTheResult()
        {
            lock (_results)
            {
                if (_results.Count <= 0)
                    return;
                
                for (int i = 0; i < _results.Count; i++)
                {
                    PathResponse pathResponse = _results.Dequeue();

                    pathResponse.callBack(pathResponse.path, pathResponse.success);
                }
            }
        }

        private void FinishProcessing(PathResponse pathResponse)
        {
            lock (_results)
            {
                _results.Enqueue(pathResponse);
            }
        }
    }
}