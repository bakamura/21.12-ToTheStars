using UnityEngine;
using UnityEngine.UI;
using Stars.Player;

namespace Stars.UI {
    public class FireGauge : MonoBehaviour {

        [Header("References")]

        [SerializeField] private Image _gaugeFill;

        private void Awake() {
            PlayerHealth.OnHealthChange.AddListener(GaugeUpdate);
        }

        private void GaugeUpdate(float value) {
            _gaugeFill.fillAmount = value / PlayerHealth.Instance.HealthMax;
        }

    }
}