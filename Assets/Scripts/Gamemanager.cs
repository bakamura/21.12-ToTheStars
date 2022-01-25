using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

    public static int coins;
    public static int highscore;
    public static int starCurrency = 100;

    public static int[] playerUpgrades = new int[5];
    public static int[] powerupUpgrades = new int[5];

    public static void RunStart() {
        // Call when starting a run
        // Create fireball and change it's values corresponding to the player upgrades
        PlayerData.Instance.maxHealth = 100 + playerUpgrades[0] * 25;
        PlayerData.Instance.ChangeHealth((float)(PlayerData.Instance.maxHealth * (0.5f + 0.05 * playerUpgrades[1])));

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

    //
    //Upgrade System
    //

    public static void UnlockPlayerUpgradeBtn(int btnNumber) {
        if (playerUpgrades[btnNumber] < 5) {
            if (coins >= UpgradeCostCalc(btnNumber)) {
                playerUpgrades[btnNumber]++;
            }
            else {
                // Show insuficient coins msg?
            }
        }
    }

    public static void UnlockPowerupUpgradeBtn(int btnNumber) {
        if (powerupUpgrades[btnNumber] < 5) {
            if (coins >= UpgradeCostCalc(btnNumber)) {
                powerupUpgrades[btnNumber]++;
            }
            else {
                // Show insuficient coins msg?
            }
        }
    }

    private static int UpgradeCostCalc(int level) {
        switch (level) {
            case 0: return 100;
            case 1: return 500;
            case 2: return 2500;
            case 3: return 7500;
            case 4: return 10000;
            default:
                Debug.LogWarning("Error Fetching Upgrade Cost");
                return 9999999;
        }
    }
}
