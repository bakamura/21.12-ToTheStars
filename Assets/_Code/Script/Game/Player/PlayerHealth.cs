using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Naka;

namespace Stars.Player {
    public class PlayerHealth : Singleton<PlayerHealth> {

        public static UnityEvent<float> OnHealthChange { get; private set; } = new UnityEvent<float>();
        public static UnityEvent OnDeath { get; private set; } = new UnityEvent();

        [Header("Health")]

        [SerializeField] private float[] _healthMax;
        private int _healthMaxUsing;
        public float HealthMax { get { return _healthMax[_healthMaxUsing]; } }
        private float _healthCurrent;

        [Header("Decay")]

        [SerializeField, Min(0)] private float _decayRate;

        private void Start() {
            StartCoroutine(Decay());
        }

        private IEnumerator Decay() {
            while (_healthCurrent > 0) {
                TakeDamage(_decayRate * Time.deltaTime);

                yield return null;
            }
        }

        public void TakeDamage(float damage) {
            if (damage >= 0) {
                _healthCurrent -= damage;
                OnHealthChange.Invoke(_healthCurrent);
                if (_healthCurrent <= 0) OnDeath.Invoke();
            }
            else Debug.LogWarning("TakeDamage called with a negative value. For healing, call 'TakeHeal'");
        }

        public void TakeHeal(float heal) {
            if (heal >= 0) {
                _healthCurrent = Mathf.Clamp(_healthCurrent + heal, 0, HealthMax);
                OnHealthChange.Invoke(_healthCurrent);
            }
            else Debug.LogWarning("TakeHeal called with a negative value. For damage, call 'TakeDamage'");

        }

    }
}