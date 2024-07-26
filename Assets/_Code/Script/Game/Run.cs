using UnityEngine.Events;

namespace Stars.Game {
    public struct Run {

        public static UnityEvent<int> OnRunScoreChange { get; private set; } = new UnityEvent<int>();
        public static UnityEvent<int> OnRunCurrencyChange { get; private set; } = new UnityEvent<int>();

        public int Score { get { return (int)_score; } }
        private float _score;
        public int Currency { get; private set; }

        public void AddScore(float amount) {
            _score += amount;
            OnRunScoreChange.Invoke(Score);
        }

        public void AddCurrency(int amount) {
            Currency += amount;
            OnRunCurrencyChange.Invoke(Currency);
        }

    }
}
