﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalCoin : MonoBehaviour {

    [SerializeField] private float _lifeIncrease;
    [SerializeField] private int _coinValue;
    //[SerializeField] private float _velocityIncrease;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            //if (!PowerUp.isPlayerFlying) {
            //SceneControl.TilesManager.Instance.VelocityChange(_velocityIncrease);
            PlayerData.Instance.ChangeHealth(_lifeIncrease);
            GameManager.coins += _coinValue + GameManager.playerUpgrades[2];
            Destroy(gameObject);
            //}
        }
    }
}
