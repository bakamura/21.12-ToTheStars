using UnityEngine;
using TMPro;
using Stars.Game;

namespace Stars.UI {
    public class ScoreCounter : MonoBehaviour {

        [Header("Reference")]

        [SerializeField] private TextMeshProUGUI _counterText;
            
        private void Awake() {
            Run.OnRunScoreChange.AddListener(CounterUpdate);
        }

        private void CounterUpdate(int amount) {
            _counterText.text = amount.ToString();
        }

    }
}