using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; } = null;

    public int coins;
    public int highscore;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        SaveSystem.LoadData();
    }

    public void RunStart() {
        // Call when starting a run
        // Create fireball and change it's values corresponding to the player upgrades
    }

    public void AtRunEnd(int coinsGain, int runScore) {
        // Call when ending a run
        coins += coinsGain;
        if (runScore > highscore) {
            highscore = runScore;
            // Do something to show it's a new highscore
        }

        SaveSystem.SaveData();
    }
}
