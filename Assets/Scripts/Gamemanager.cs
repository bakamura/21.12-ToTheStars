﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; } = null;

    public int coins;
    public int starCoins;
    public int highscore;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        SaveSystem.LoadData();
    }

    public void RunStart() {
        // Call when starting a run
        // Create fireball and change it's values corresponding to the player upgrades
        PlayerData.Instance.maxHealth = 100 + UpgradeManager.Instance.playerUpgrades[0] * 25;
        PlayerData.Instance.ChangeHealth((float) (PlayerData.Instance.maxHealth * (0.5f + 0.05 * UpgradeManager.Instance.playerUpgrades[1])));

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
