using UnityEngine;
using TMPro;
using Stars.Game;

namespace Stars.UI {
    public class ScoreDisplay : MonoBehaviour {

        [Header("Reference")]

        [SerializeField] private TextMeshProUGUI _displayText;
            
        private void Awake() {
            Run.OnRunScoreChange.AddListener(DisplayUpdate);
        }

        private void DisplayUpdate(int amount) {
            _displayText.text = amount.ToString();
        }

    }
}