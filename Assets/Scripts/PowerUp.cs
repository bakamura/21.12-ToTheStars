using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [NonSerialized] public SpriteRenderer srPowerUp;

    private const int _totalPowerUps = 2; // @
    public enum PowerUpType {
        PowerUp1,
        PowerUp2
    };
    public PowerUpType _powerUpType;
    [SerializeField] private Sprite[] _iconsList = new Sprite[_totalPowerUps]; // @
    [SerializeField] private float[] _durationsList = new float[_totalPowerUps]; // @

    // Functions similar to delegates, but writes simpler in code.
    private Action OnCollectPowerUp;
    [NonSerialized] public Action OnPowerUpExpires;

    [NonSerialized] public float powerUpDuration;

    private void Awake() {
        srPowerUp = GetComponent<SpriteRenderer>();
        PowerUpSetup();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (PowerUpHUDManager.Instance.CreatePowerUpInUI(this)) OnCollectPowerUp.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void PowerUpSetup() {
        RandomizePowerUpType();
        srPowerUp.sprite = _iconsList[(int)_powerUpType];
        powerUpDuration = _durationsList[(int)_powerUpType];
        SetPowerUpActions();
    }

    private void RandomizePowerUpType() {
        int randomNumber = UnityEngine.Random.Range(0, _totalPowerUps * 2);
        switch (randomNumber) {
            case 0:
                _powerUpType = PowerUpType.PowerUp1;
                break;
            case 1:
                _powerUpType = PowerUpType.PowerUp2;
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }

    private void SetPowerUpActions() {
        switch (_powerUpType) {
            case PowerUpType.PowerUp1:
                OnCollectPowerUp = StartPowerUp1;
                OnPowerUpExpires = FinishPowerUp1;
                break;
            case PowerUpType.PowerUp2:
                OnCollectPowerUp = StartPowerUp2;
                OnPowerUpExpires = FinishPowerUp2;
                break;
        }
    }

    private void StartPowerUp1() {
        Debug.Log("PowerUP1");
    }

    private void StartPowerUp2() {
        Debug.Log("PowerUP2");
    }

    private void FinishPowerUp1() {
        Debug.Log("FinishPowerUP1");
    }

    private void FinishPowerUp2() {
        Debug.Log("FinishPowerUP2");
    }
}
