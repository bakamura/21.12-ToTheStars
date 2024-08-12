using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Naka;
using Naka.UI;

namespace Stars.UI {
    public class SceneTransition : Singleton<SceneTransition> {

        [Header("References")]

        [SerializeField] private Menu _transitionCover;

        [Header("Cache")]

        private WaitForSeconds _coverWait;

        protected override void Awake() {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            _coverWait = new WaitForSeconds(_transitionCover.TransitionDuration);
        }

        public void GoToScene(string sceneName) {
            StartCoroutine(GoToSceneRoutine(sceneName));
        }

        private IEnumerator GoToSceneRoutine(string sceneName) {
            _transitionCover.Open();

            yield return _coverWait;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            yield return asyncOperation;

            _transitionCover.Close();
        }

    }
}
