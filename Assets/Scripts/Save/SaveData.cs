using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public int coins;
    public int highscore;
    public int[] playerUpgrades;
    public int[] powerupUpgrades;
    public bool[] constellation1;
    public bool[] constellation2;
    public bool[] constellation3;
    public bool[] constellation4;
    public bool[] constellation5;

    public SaveData() {
        coins = GameManager.coins;
        highscore = GameManager.highscore;

        playerUpgrades = GameManager.playerUpgrades; //
        powerupUpgrades = GameManager.powerupUpgrades; //

        constellation1 = ConstellationManager.Instance.nameoneConstellation.stars; //
        constellation2 = ConstellationManager.Instance.nametwoConstellation.stars; //
        constellation3 = ConstellationManager.Instance.namethreeConstellation.stars; //
        constellation4 = ConstellationManager.Instance.namefourConstellation.stars; //
        constellation5 = ConstellationManager.Instance.namefiveConstellation.stars; //
    }

}
