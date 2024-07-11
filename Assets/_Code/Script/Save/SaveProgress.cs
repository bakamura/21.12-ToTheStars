
using UnityEngine;

namespace Stars.Save {
    public struct SaveProgress {

        public int HighScore { get; private set; }
        public int currency;
        // Shop Upgrades
        // Constelations

        public void SetHighScore(int newHighScore) {
            if (newHighScore > HighScore) HighScore = newHighScore;
            else Debug.LogWarning("Prevented setting highscore lower than current");
        }

    }
}