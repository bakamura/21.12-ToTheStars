using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public static UpgradeManager Instance { get; private set; }

    ////public int[] playerUpgrades = new int[5];
    ////public int[] powerupUpgrades = new int[5];

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }

    ////public void UnlockPlayerUpgradeBtn(int btnNumber) {
    ////    if (playerUpgrades[btnNumber] < 5) {
    ////        if (GameManager.coins >= UpgradeCostCalc(btnNumber)) {
    ////            playerUpgrades[btnNumber]++;
    ////        }
    ////        else {
    ////            // Show insuficient coins msg?
    ////        }
    ////    }
    ////}

    ////public void UnlockPowerupUpgradeBtn(int btnNumber) {
    ////    if (powerupUpgrades[btnNumber] < 5) {
    ////        if (GameManager.coins >= UpgradeCostCalc(btnNumber)) {
    ////            powerupUpgrades[btnNumber]++;
    ////        }
    ////        else {
    ////            // Show insuficient coins msg?
    ////        }
    ////    }
    ////}

    ////private int UpgradeCostCalc(int level) {
    ////    switch (level) {
    ////        case 0: return 100;
    ////        case 1: return 500;
    ////        case 2: return 2500;
    ////        case 3: return 7500;
    ////        case 4: return 10000;
    ////        default:
    ////            Debug.LogWarning("Error Fetching Upgrade Cost");
    ////            return 9999999;
    ////    }
    ////}

}
