using UnityEngine;
using Stars.UI;

namespace Stars {
    public class SceneTransitionRef : MonoBehaviour {

        public void GoToScene(string sceneName) {
            SceneTransition.Instance.GoToScene(sceneName);
        }

    }
}
