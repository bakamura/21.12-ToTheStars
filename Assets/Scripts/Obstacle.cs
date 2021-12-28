using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] private float _damage;
    //[SerializeField] private float _velocityReduction;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerData.Instance.ChangeHealth(-_damage);
            //SceneControl.TilesManager.Instance.VelocityChange(-_velocityReduction);
            if(PlayerData.Instance.currentHealth > 0) Destroy(gameObject);
        }

    }
}
