using UnityEngine;
using Stars.Player;

namespace Stars.Game {
    public class Obstacle : Collidable {

        [Header("Parameters")]

        [SerializeField, Min(0f)] private float _damage;
        [SerializeField, Min(0)] private int _currencyGiven;

        protected override void Collide() {
            PlayerHealth.Instance.TakeDamage(_damage);
            RunController.Instance.CurrentRun.AddCurrency(_currencyGiven);
        }

    }
}
