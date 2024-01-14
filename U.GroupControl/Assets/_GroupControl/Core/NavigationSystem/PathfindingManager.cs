using System;
using System.Collections.Generic;
using _GroupControl.Core.NavigationSystem.Utilities;
using GameSystems.Navigation.Types;
using UnityEngine;

namespace _GroupControl.Core.NavigationSystem
{
    public class PathfindingManager : MonoBehaviour
    {
        private NavigationGrid _grid;
        public static PathfindingManager Instance { get; private set; }

        public void FindPath(PathRequest request, Action<PathResponse> callback)
        {
            if (_grid == null || _grid.Grid == null)
                return;

            Heap<Node> openList = new Heap<Node>();
            List<Node> closeList = new List<Node>();

            Node characterNode = _grid.GetPointNodeFromGridByPosition(request.startNode);
            Node targetNode = _grid.GetPointNodeFromGridByPosition(request.targetNode);

            if (targetNode == null)
                return;

            openList.Add(characterNode);

            PathResponse pathResponse = PathfindingUtils.SearchPath(targetNode, openList, closeList, _grid);

            Vector3[] path = PathfindingUtils.CreatePath(characterNode, targetNode);
            pathResponse.path = path;
            pathResponse.callBack = request.callBack;

            callback(pathResponse);
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _grid = GetComponent<NavigationGrid>();
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}
