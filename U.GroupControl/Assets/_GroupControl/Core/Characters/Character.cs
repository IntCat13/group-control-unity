using System.Collections;
using _GroupControl.Core.Characters.CharacterUtils;
using _GroupControl.Core.Characters.Types;
using _GroupControl.Core.NavigationSystem;
using _GroupControl.Core.NavigationSystem.Types;
using UnityEngine;
using CharacterInfo = _GroupControl.Core.Characters.Types.CharacterInfo;
using Random = UnityEngine.Random;

namespace _GroupControl.Core.Characters
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private CharacterInfo _info;
        private NavigationInfo _navigationInfo;
        private Transform _leader;
        private Color _pathColor;
        private Vector3 _previousTarget;
        private bool _isRecoveringStamina;

        public Transform CurrentPoint => transform;

        private void Awake()
        {
            _pathColor = new Color(Random.value, Random.value, Random.value);
        }

        public void SetLeader(Transform leader)
        {
            _leader = leader;

            if (_leader != null)
                StartCoroutine(SetDestinationCoroutine());
            else
                StopCoroutine(SetDestinationCoroutine());
        }

        public void Initialize(CharacterInfo info)
        {
            _info = info;
        }

        public void SetDestination(Vector3 newTarget)
        {
            if (ShouldSkipDestination(newTarget))
                return;

            SetDestinationRoute(newTarget);
        }

        private bool ShouldSkipDestination(Vector3 newTarget)
            => _info.StoppingDistance.IsDistanceReached(_previousTarget, newTarget);

        private void SetDestinationRoute(Vector3 newTarget)
        {
            _previousTarget = newTarget;
            PathRequestService.Instance.Request(new PathRequest(transform.position, newTarget, OnRequestReceived));
        }

        private void Update()
        {
            HandleStamina();
            if (_navigationInfo.TargetPath == null) return;

            if (_isRecoveringStamina)
                return;
            
            HandleLeaderMovement();

            if (_leader == null)
                _navigationInfo = _navigationInfo.MoveToTarget(transform, _info);
        }
        
        private void HandleStamina()
        {
            if (_navigationInfo.TargetPath == null || _isRecoveringStamina)
                RecoverStamina();
            else
                ConsumeStamina();
        }
        
        private void ConsumeStamina()
        {
            _info.Stamina -= Time.deltaTime * _info.Speed;
            _info.Stamina = Mathf.Clamp(_info.Stamina, 0f, _info.MaxStamina);

            if (_info.Stamina <= 0f)
                _isRecoveringStamina = true;
        }
        
        private void RecoverStamina()
        {
            _info.Stamina += Time.deltaTime * _info.MaxStamina/_info.StaminaRecoveryTime;
            _info.Stamina = Mathf.Clamp(_info.Stamina, 0f, _info.MaxStamina);

            if (_info.Stamina >= _info.MaxStamina)
                _isRecoveringStamina = false;
        }
        
        private void HandleLeaderMovement()
        {
            if (_leader != null && Vector3.Distance(_leader.position, transform.position) >= _info.LeaderStoppingDistance)
                _navigationInfo = _navigationInfo.MoveToTarget(transform, _info);
        }

        private void OnRequestReceived(Vector3[] path, bool success)
        {
            _navigationInfo = new NavigationInfo(path);
        }

        IEnumerator SetDestinationCoroutine()
        {
            while (_leader != null)
            {
                HandleLeaderDestination();

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void HandleLeaderDestination()
        {
            if (Vector3.Distance(_leader.position, transform.position) >= _info.LeaderStoppingDistance)
                SetDestination(_leader.position);
            else
                _navigationInfo = new NavigationInfo(null);
            
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _navigationInfo.DrawPath(_pathColor);
        }
#endif
    }
}