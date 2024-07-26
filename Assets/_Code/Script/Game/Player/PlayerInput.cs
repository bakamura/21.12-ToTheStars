using UnityEngine;
using UnityEngine.Events;
using Naka;

namespace Stars.Player {
    public class PlayerInput : Singleton<PlayerInput> {

        public UnityEvent<bool> OnChangeLaneInput { get; private set; } = new UnityEvent<bool>();

        [Header("Mobile Input")]

        [Tooltip("Percentage of Height")]
        [SerializeField, Range(0f, 0.75f)] private float _inputTreshould;
        private float _inputTreshouldPixels;
        private Vector2 _touchStartPoint;

        [Header("Cache")]

        private float _heightDifferenceCache;

        protected override void Awake() {
            base.Awake();

            _inputTreshouldPixels = _inputTreshould * Screen.height;
        }

        private void Update() {
            MobileInput();
#if UNITY_EDITOR
            PCInput();
#endif
        }

        private void MobileInput() {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase) {
                    case TouchPhase.Began:
                        _touchStartPoint = touch.position;
                        break;
                    case TouchPhase.Moved:
                        if (_touchStartPoint.x > 0) {
                            _heightDifferenceCache = _touchStartPoint.y - touch.position.y;
                            if (Mathf.Abs(_heightDifferenceCache) > _inputTreshouldPixels) {
                                OnChangeLaneInput.Invoke(_heightDifferenceCache > 0);
                                _touchStartPoint = Vector2.left;
                            }
                        }
                        break;
                    case TouchPhase.Ended:
                        _touchStartPoint = Vector2.left;
                        break;
                }
            }
        }

#if UNITY_EDITOR
        private void PCInput() {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) OnChangeLaneInput.Invoke(true);
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) OnChangeLaneInput.Invoke(false);
        }
#endif
    }
}