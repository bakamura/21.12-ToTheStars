using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [NonSerialized] public SpriteRenderer srPowerUp;

    private const int _totalPowerUps = 5; // @
    public enum PowerUpType {
        Magnet,
        PowderKeg,
        Invincibility,
        Shield,
        CoinMultiplier
    };
    public PowerUpType _powerUpType;
    [SerializeField] private Sprite[] _iconsList = new Sprite[_totalPowerUps]; // @
    [SerializeField] private float[] _baseDurationsList = new float[_totalPowerUps]; // @
    [SerializeField] private float _powderKegSpeedIncrease;
    [NonSerialized] public static float maxSizePlayerIncreasePowderKeg = 3;
    [SerializeField] private float _shieldDamageReduction;
    [SerializeField] private int _coinMultiplier;

    public float areaKegEff; //

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
            //Destroy(this.gameObject);
        }
    }

    public void PowerUpSetup() {
        //RandomizePowerUpType();
        srPowerUp.sprite = _iconsList[(int)_powerUpType];
        powerUpDuration = _baseDurationsList[(int)_powerUpType];
        SetPowerUpActions();
    }

    private void RandomizePowerUpType() {
        int randomNumber = UnityEngine.Random.Range(0, _totalPowerUps * 2);
        switch (randomNumber) {
            case 0:
                _powerUpType = PowerUpType.Magnet;
                break;
            case 1:
                _powerUpType = PowerUpType.PowderKeg;
                break;
            case 2:
                _powerUpType = PowerUpType.Invincibility;
                break;
            case 3:
                _powerUpType = PowerUpType.Shield;
                break;
            case 4:
                _powerUpType = PowerUpType.CoinMultiplier;
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }

    private void SetPowerUpActions() {
        switch (_powerUpType) {
            case PowerUpType.Magnet:
                OnCollectPowerUp = StartMagnet;
                OnPowerUpExpires = FinishMagnet;
                break;
            case PowerUpType.PowderKeg:
                OnCollectPowerUp = StartPowderKeg;
                OnPowerUpExpires = FinishPowderKeg;
                break;
            case PowerUpType.Invincibility:
                OnCollectPowerUp = StartInvincibility;
                OnPowerUpExpires = FinishInvincibility;
                break;
            case PowerUpType.Shield:
                OnCollectPowerUp = StartShield;
                OnPowerUpExpires = FinishShield;
                break;
            case PowerUpType.CoinMultiplier:
                OnCollectPowerUp = StartCoinMultipier;
                OnPowerUpExpires = FinishCoinMultiplier;
                break;
        }
    }

    private void StartMagnet() {
        PlayerData.Instance.magneticEffect.SetActive(true);
    }

    private void FinishMagnet() {
        PlayerData.Instance.magneticEffect.GetComponent<MagneticEffect>().ClearList();
        PlayerData.Instance.magneticEffect.SetActive(false);
    }

    private void StartPowderKeg() {
        GameObject[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(areaKegEff, 10), Vector2.zero);
        foreach (GameObject hit in hits) if (hit.tag == "Obstacle") Destroy(hit);

        PlayerData.Instance.powderKegEffect.SetActive(true);


        //a way to make the canon upgrade
        //PlayerData.Instance.isFlying = true;
        //MapGenerator.Instance._powderKegSpeedIncrease = _powderKegSpeedIncrease;
        //PlayerMovement.Instance.ChangeSize(true);
    }

    private void FinishPowderKeg() {
        PlayerData.Instance.powderKegEffect.SetActive(false);
        //a way to make the canon upgrade
        //PlayerMovement.Instance.ChangeSize(false);
        //MapGenerator.Instance._powderKegSpeedIncrease = 0f;
        //PlayerData.Instance.isFlying = false;
    }

    private void StartInvincibility() {
        PlayerData.Instance.isInvincible = true;
    }

    private void FinishInvincibility() {
        PlayerData.Instance.isInvincible = false;
    }
    private void StartShield() {
        PlayerData.Instance.shieldDamageReduction = _shieldDamageReduction;
    }

    private void FinishShield() {
        PlayerData.Instance.shieldDamageReduction = 0;
    }
    private void StartCoinMultipier() {
        PlayerData.Instance.coinMultiplierPowerUp = _coinMultiplier;
    }

    private void FinishCoinMultiplier() {
        PlayerData.Instance.coinMultiplierPowerUp = 1;
    }
}