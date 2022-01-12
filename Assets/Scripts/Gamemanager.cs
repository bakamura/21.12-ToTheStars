using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

    public static int coins;
    public static int highscore;
    public static int starCoins;

    public static void RunStart() {
        // Call when starting a run
        // Create fireball and change it's values corresponding to the player upgrades
        PlayerData.Instance.maxHealth = 100 + UpgradeManager.Instance.playerUpgrades[0] * 25;
        PlayerData.Instance.ChangeHealth((float) (PlayerData.Instance.maxHealth * (0.5f + 0.05 * UpgradeManager.Instance.playerUpgrades[1])));

    }

    public static void AtRunEnd(int coinsGain, int runScore) {
        // Call when ending a run
        coins += coinsGain;
        if (runScore > highscore) {
            highscore = runScore;
            // Do something to show it's a new highscore
        }

        SaveSystem.SaveData();
    }
}
