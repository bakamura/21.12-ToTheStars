
using UnityEngine.Events;

namespace Stars.Game {
    public struct Run {

        public static UnityEvent<int> OnRunScoreChange { get; private set; } = new UnityEvent<int>();
        public static UnityEvent<int> OnRunCurrencyChange { get; private set; } = new UnityEvent<int>();

        public int Score { get; private set; }
        public int Currency { get; private set; }

        public void AddScore(int amount) {
            Score += amount;
            OnRunScoreChange.Invoke(Score);
        }

        public void AddCurrency(int amount) {
            Currency += amount;
            OnRunCurrencyChange.Invoke(Currency);
        }

    }
}
