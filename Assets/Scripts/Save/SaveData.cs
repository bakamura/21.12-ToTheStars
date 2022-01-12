using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public int coins;
    public int highscore;
    public int[] playerUpgrades;
    public int[] powerupUpgrades;

    public SaveData() {
        coins = GameManager.coins;
        highscore = GameManager.highscore;

        //playerUpgrades = GameManager.Instance.playerUpgrades;
        //powerupUpgrades = GameManager.Instance.powerupUpgrades;
    }

}
