using UnityEngine;

namespace Stars.Game {
    public abstract class Collidable : MonoBehaviour {

        protected abstract void Collide();

        protected void OnTriggerEnter2D(Collider2D collider) {
            Collide();
            Destroy(gameObject); // Provisory
        }

    }
}
