using Stars.Player;
using UnityEngine;

namespace Stars {
    public class Parallax : MonoBehaviour {

        [Header("Parameters")]

        [SerializeField] private SpriteRenderer _parallaxedObject;
        [SerializeField] private float _speed;
        [SerializeField] private bool _multipliedByPlayerSpeed;
        private float _width;

        private void Awake() {
            _width = _parallaxedObject.sprite.texture.width / _parallaxedObject.sprite.pixelsPerUnit;
        }

        private void Update() {
            transform.position -= _speed * (_multipliedByPlayerSpeed ? PlayerMovement.Instance.SpeedCurrent : 1f) * Time.deltaTime * Vector3.right;
            if (Mathf.Abs(transform.position.x) >= _width) transform.position -= Mathf.Sign(transform.position.x) * _width * Vector3.right;
        }

    }
}
