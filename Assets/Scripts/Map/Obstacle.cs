using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] private float _damage;
    //[SerializeField] private int _coinValue;
    //[SerializeField] private float _velocityReduction;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (!collision.GetComponent<PlayerData>().isFlying) {
                if (!collision.GetComponent<PlayerData>().isInvincible){
                    PlayerData.Instance.ChangeHealth(-(_damage - _damage * PlayerData.Instance.shieldDamageReduction));
                    //finishes the shield power up
                    if(PlayerData.Instance.shieldDamageReduction > 0) PowerUpHUDManager.Instance._iconsInScene[(int)PowerUp.PowerUpType.Shield].ChangeDuration(-1);
                }
                //finishes the invincibility power up
                else PowerUpHUDManager.Instance._iconsInScene[(int)PowerUp.PowerUpType.Invincibility].ChangeDuration(-1);
                //SceneControl.TilesManager.Instance.VelocityChange(-_velocityReduction);
                if (PlayerData.Instance.currentHealth > 0) Destroy(gameObject);
            }
        }

    }
}
