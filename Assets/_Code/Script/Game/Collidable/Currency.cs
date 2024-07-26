using UnityEngine;
using Stars.Player;

namespace Stars.Game {
    public class Currency : Collidable {

        [Header("Parameters")]

        [SerializeField, Min(0f)] private float _heal;
        [SerializeField, Min(0)] private int _scoreGiven;
        [SerializeField, Min(0)] private int _currencyGiven;

        protected override void Collide() {
            PlayerHealth.Instance.TakeHeal(_heal);
            RunController.Instance.CurrentRun.AddScore(_scoreGiven);
            RunController.Instance.CurrentRun.AddCurrency(_currencyGiven);
        }

    }
}