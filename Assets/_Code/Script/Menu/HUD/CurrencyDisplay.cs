using UnityEngine;
using TMPro;
using Stars.Game;

namespace Stars.UI {
    public class CurrencyDisplay : MonoBehaviour {

        [Header("Reference")]

        [SerializeField] private TextMeshProUGUI _displayText;
            
        private void Awake() {
            Run.OnRunCurrencyChange.AddListener(DisplayUpdate);
        }

        private void DisplayUpdate(int amount) {
            _displayText.text = amount.ToString();
        }

    }
}