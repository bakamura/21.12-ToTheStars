using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalScript : MonoBehaviour {

    [SerializeField] private float _lifeIncrease;
    //[SerializeField] private float _velocityIncrease;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerData.Instance.ChangeHealth(_lifeIncrease);
            //SceneControl.TilesManager.Instance.VelocityChange(_velocityIncrease);
            Destroy(gameObject);
        }
    }
}
