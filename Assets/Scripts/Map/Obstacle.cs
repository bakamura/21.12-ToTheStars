using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] private float _damage;
    //[SerializeField] private int _coinValue;
    //[SerializeField] private float _velocityReduction;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            //if (!PowerUp.isPlayerFlying) {
                if (!PowerUp.isPlayerInvincible){
                    PlayerData.Instance.ChangeHealth(-(_damage - _damage * PowerUp._shieldDamageReduction));
                    //finishes the shield power up
                    if(PowerUp._shieldDamageReduction > 0) PowerUpHUDManager.Instance._iconsInScene[(int)PowerUp.PowerUpType.Shield].ChangeDuration(-1);
                }
                //finishes the invincibility power up
                else PowerUpHUDManager.Instance._iconsInScene[(int)PowerUp.PowerUpType.Invincibility].ChangeDuration(-1);
                //SceneControl.TilesManager.Instance.VelocityChange(-_velocityReduction);
                if (PlayerData.Instance.currentHealth > 0) Destroy(gameObject);
            //}
        }

    }
}
