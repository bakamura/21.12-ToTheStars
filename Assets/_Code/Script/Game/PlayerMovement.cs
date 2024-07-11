using UnityEngine;

namespace Stars.Player {
    public class PlayerMovement : MonoBehaviour {

        [Header("Parameters")]

        [SerializeField] private float _switchLaneSpeedBase;
        private bool? _queuedMove;

        [SerializeField] private float[] _laneHeights;
        private int _laneCurrent;

        [Header("Cache")]

        private Vector3 _positionCache;
        private float _heightDifferenceCache;
        private float _heightMovementFrameCache;

        private void Awake() {
            _laneCurrent = _laneHeights.Length / 2;
            _positionCache = transform.position;
        }

        private void Start() {
            PlayerInput.Instance.OnChangeLaneInput.AddListener(SwitchLane);
        }

        private void Update() {
            if (transform.position.y != _laneHeights[_laneCurrent]) {
                _heightDifferenceCache = _laneHeights[_laneCurrent] - transform.position.y;
                _heightMovementFrameCache = Mathf.Sign(_heightDifferenceCache) * _switchLaneSpeedBase * Time.deltaTime;
                _positionCache[1] = Mathf.Abs(_heightMovementFrameCache) < Mathf.Abs(_heightDifferenceCache) ? _heightMovementFrameCache : _laneHeights[_laneCurrent];
                transform.position = _positionCache;
            }
            else if (_queuedMove != null) {
                _laneCurrent = Mathf.Clamp(_laneCurrent + (_queuedMove.Value ? 1 : -1), 0, _laneHeights.Length - 1);
                _queuedMove = null;
            }
        }

        private void SwitchLane(bool isUpwards) {
            _queuedMove = isUpwards;
        }

    }
}