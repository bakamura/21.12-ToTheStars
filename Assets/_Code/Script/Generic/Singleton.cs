using UnityEngine;

namespace Naka {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

        public static T Instance { get; private set; }

        protected virtual void Awake() {
            if (Instance == null) Instance = this as T;
            else {
                Debug.LogWarning($"Destroying duplicate instance of Singleton '{typeof(T).Name}'");
                Destroy(gameObject);
            }
        }

    }
}