using System.Collections;
using UnityEngine;

namespace Naka.UI {
    public class Fade : Menu {

        [SerializeField] private bool _interactable;

        public override void Open() {
            StartCoroutine(OpenRoutine());
        }

        public override void Close() {
            StartCoroutine(CloseRoutine());
        }

        private IEnumerator OpenRoutine() {
            float currentDuration = 0f;
            while (currentDuration < 1f) {
                currentDuration += Time.deltaTime / _transitionDuration;
                _canvasGroup.alpha = _transitionCurve.Evaluate(currentDuration);

                yield return null;
            }
            if (_interactable) {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }
        }

        private IEnumerator CloseRoutine() {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            float currentDuration = 1f;
            while (currentDuration > 0f) {
                currentDuration -= Time.deltaTime / _transitionDuration;
                _canvasGroup.alpha = _transitionCurve.Evaluate(currentDuration);

                yield return null;
            }
        }

    }
}