using UnityEngine;
using TMPro;
using Stars.Game;

namespace Stars.UI {
    public class CurrencyCounter : MonoBehaviour {

        [Header("Reference")]

        [SerializeField] private TextMeshProUGUI _counterText;
            
        private void Awake() {
            Run.OnRunCurrencyChange.AddListener(CounterUpdate);
        }

        private void CounterUpdate(int amount) {
            _counterText.text = amount.ToString();
        }

    }
}