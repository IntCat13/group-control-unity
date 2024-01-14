using _GroupControl.Core.NavigationSystem;
using GameSystems.Navigation.Types;
using UnityEngine;

namespace _GroupControl.Core.Characters
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float speed = 3f;
        [SerializeField] private float stoppingDistance = 0.5f;

        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float rotationCompleteThreshold = 1f;

        [Header("Target Settings")]
        [SerializeField] private float heightAboveSurface = 0.1f;

        private Camera _camera;
        private Vector3[] _targetPath;
        private int _indexPath = 0;

        private Color pathColor;
        private Vector3 _currentPosition;

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_targetPath != null && _targetPath.Length > 0)
            {
                Gizmos.color = Color.clear;
                Gizmos.DrawLine(transform.position, _targetPath[0]);

                for (int i = 0; i < _targetPath.Length - 1; i++)
                {
                    if (Vector3.Distance(_currentPosition, _targetPath[i + 1]) > 0.05f)
                    {
                        Gizmos.color = pathColor;
                        Gizmos.DrawLine(_targetPath[i], _targetPath[i + 1]);
                    }
                }
            }
        }
    #endif

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetNewTarget();
            }

            if (_targetPath == null)
            {
                return;
            }

            MoveToTarget();
        }

        void MoveToTarget()
        {
            if (_indexPath >= _targetPath.Length)
            {
                return;
            }

            RoateToTarget(_targetPath[_indexPath]);

            float distanceToTheNextWayPoint = Vector3.Distance(transform.position, _targetPath[_indexPath]);
            float distanceToFinaltWayPoint = Vector3.Distance(transform.position, _targetPath[_targetPath.Length - 1]);

            if (distanceToTheNextWayPoint < stoppingDistance)
            {
                _indexPath++;
            }

            if (distanceToFinaltWayPoint < stoppingDistance)
            {
                _indexPath = _targetPath.Length;
            }
            else if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(_targetPath[_indexPath] - transform.position)) < rotationCompleteThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPath[_indexPath], speed * Time.deltaTime);
            }
        }

        void RoateToTarget(Vector3 target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        void SetNewTarget()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("floor"))
            {
                Vector3 hitPoint = hit.point;
                Vector3 hitNormal = hit.normal;

                Vector3 newTarget = hitPoint + hitNormal * heightAboveSurface;

                PathRequest pathRequest = new PathRequest(transform.position, newTarget, OnRequestReceived);
                PathRequestService.Instance.Request(pathRequest);
            }
        }

        public void OnRequestReceived(Vector3[] path, bool success)
        {
            _targetPath = path;
            _indexPath = 0;
            
            pathColor = Random.ColorHSV();
        }
    }
}
