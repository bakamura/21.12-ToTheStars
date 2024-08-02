using System.Collections;
using UnityEngine;
using Stars.Player;
using UnityEngine.Events;

namespace Stars.Game {
    public class RunStarter : MonoBehaviour {

        public static UnityEvent OnPlayerEntryEnd { get; private set; } = new UnityEvent();

        [Header("Parameters")]

        [SerializeField] private Vector2 _playerEntryStartPoint;
        private Vector2 _playerEntryEndPoint;
        [SerializeField] private float _playerEntryDuration;

        private void Start() {
            _playerEntryEndPoint = PlayerMovement.Instance.transform.position;
            StartCoroutine(AnimatePlayerEntry());
        }

        private IEnumerator AnimatePlayerEntry() {
            float entryPoint = 0;
            while (entryPoint < 1) {
                entryPoint += Time.deltaTime / _playerEntryDuration;
                PlayerMovement.Instance.transform.position = Vector2.Lerp(_playerEntryStartPoint, _playerEntryEndPoint, entryPoint);

                yield return null;
            }

            OnPlayerEntryEnd.Invoke();
        }

    }
}
