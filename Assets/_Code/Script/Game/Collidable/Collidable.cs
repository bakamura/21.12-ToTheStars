using UnityEngine;

namespace Stars.Game {
    public abstract class Collidable : MonoBehaviour {

        protected abstract void Collide();

        protected void OnCollisionEnter2D(Collision2D collision) {
            Collide();
        }

    }
}
