using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalCoin : MonoBehaviour {

    [SerializeField] private float _lifeIncrease;
    public static int _coinValue = 1;
    //[SerializeField] private float _velocityIncrease;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (!LaunchPlayerPassive.isPlayerFlying) {
                //SceneControl.TilesManager.Instance.VelocityChange(_velocityIncrease);

                PlayerData.Instance.ChangeHealth(_lifeIncrease);

                int coinAmount = (_coinValue + GameManager.playerUpgrades[2]);
                if (GameManager.dailyDoubleCoins > 0) {
                    coinAmount *= 2;
                    GameManager.dailyDoubleCoins -= coinAmount;
                }
                GameManager.coins += coinAmount;
                HudManager.Instance.ChangeRunCoins(coinAmount);

                Destroy(gameObject);
            }
        }
    }
}
