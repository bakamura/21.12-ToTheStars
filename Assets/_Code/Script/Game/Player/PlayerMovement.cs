using UnityEngine;
using Naka;
using Stars.Game;

namespace Stars.Player {
    public class PlayerMovement : Singleton<PlayerMovement> {

        [Header("Parameters")]

        [SerializeField, Min(0f)] private float _switchLaneDurationBase;
        private bool? _queuedMove;

        [SerializeField, Min(0f)] private float _speedMax;
        public float SpeedCurrent { get { return _speedMax * (_speedRate / (_speedRate + (_speedMax * 0.8f))); } } // FIX CALC TO START AT 1
        // CHATGPT SUGGESTION
        //public float CalculateCurrentSpeed(float rateCurrent) {
        //    if (rateCurrent < 0) return 1;

        //    // Use an exponential formula to calculate diminishing returns
        //    float exponent = (float)Math.Log(0.8) / TimeToFourFifths;
        //    return 1 + (SpeedMax - 1) * (1 - (float)Math.Exp(exponent * rateCurrent));
        //}
        private float _speedRate;
        [SerializeField, Min(0f)] private float _timeToReachFourFifths;

        [SerializeField] private float[] _laneHeights;
        private int _laneCurrent;

        [Header("Cache")]

        private Vector3 _positionCache;
        private float _heightDifferenceCache;
        private float _heightMovementFrameCache;

        protected override void Awake() {
            base.Awake();

            _laneCurrent = _laneHeights.Length / 2;
            _positionCache = transform.position;
        }

        private void Start() {
            PlayerInput.Instance.OnChangeLaneInput.AddListener(SwitchLane);
        }

        private void Update() {
            if (RunController.Instance.IsInRun) {
                SwitchLaneUpdate();
                SpeedUpdate();
                //Debug.Log($"SpeedCurrent {SpeedCurrent}");
            }
        }

        private void SwitchLaneUpdate() {
            if (transform.position.y != _laneHeights[_laneCurrent]) {
                _positionCache[1] = Mathf.MoveTowards(_positionCache[1], _laneHeights[_laneCurrent], Time.deltaTime / _switchLaneDurationBase);
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

        private void SpeedUpdate() {
            _speedRate += Time.deltaTime;
        }

    }
}