using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [NonSerialized] public SpriteRenderer srPowerUp;

    private const int _totalPowerUps = 6; // @
    public enum PowerUpType {
        Magnet,
        PowderKeg,
        Invincibility,
        Shield,
        CoinMultiplier,
        StarCoin
    };
    public PowerUpType _powerUpType;
    [NonSerialized] public static int generateChance = 1;

    [SerializeField] private float[] _powerUpChance = new float[5];
    [SerializeField] private Sprite[] _iconsList = new Sprite[_totalPowerUps]; // @
    [SerializeField] private float[] _baseDurationsList = new float[5];

    [SerializeField] private float _MagnetPullForce;
    [NonSerialized] public static float _shieldDamageReduction = 1f;
    [NonSerialized] public static int _coinMultiplier = 1;
    [NonSerialized] public static bool isPlayerInvincible = false;

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
        if (collision.tag == "Player" && !LaunchPlayerPassive.isPlayerFlying) {
            if (PowerUpHUDManager.Instance.CreatePowerUpInUI(this)) OnCollectPowerUp.Invoke();
        }
    }

    public void PowerUpSetup() {
        RandomizePowerUpType();
        srPowerUp.sprite = _iconsList[(int)_powerUpType];
        powerUpDuration = _powerUpType != PowerUpType.StarCoin ? _baseDurationsList[(int)_powerUpType] * (1 + 0.5f * GameManager.powerupUpgrades[(int)_powerUpType]) : 0;
        SetPowerUpActions();
    }

    private PowerUpType SetPowerUpType(int i) {
        switch (i) {
            case 0:
                return _powerUpType = PowerUpType.Magnet;
            case 1:
                return _powerUpType = PowerUpType.PowderKeg;
            case 2:
                return _powerUpType = PowerUpType.Invincibility;
            case 3:
                return _powerUpType = PowerUpType.Shield;
            case 4:
                return _powerUpType = PowerUpType.CoinMultiplier;
            case 5:
                return _powerUpType = PowerUpType.StarCoin;
            default:
                Debug.LogWarning("Faliuer at randomizing power up");
                return _powerUpType = PowerUpType.Magnet;
        }
    }

    private void RandomizePowerUpType() {
        int randomNumber = UnityEngine.Random.Range(0, 4);
        if (randomNumber >= generateChance) {
            randomNumber = UnityEngine.Random.Range(0, 100);
            switch (randomNumber) {
                case int i when i < _powerUpChance[0]://
                    SetPowerUpType((int)PowerUpType.Magnet);
                    break;
                case int i when i < _powerUpChance[1]:
                    SetPowerUpType((int)PowerUpType.PowderKeg);
                    break;
                case int i when i < _powerUpChance[2]:
                    SetPowerUpType((int)PowerUpType.Invincibility);
                    break;
                case int i when i < _powerUpChance[3]:
                    SetPowerUpType((int)PowerUpType.Shield);
                    break;
                case int i when i < _powerUpChance[4]:
                    SetPowerUpType((int)PowerUpType.CoinMultiplier);
                    break;
                default:
                    SetPowerUpType((int)PowerUpType.StarCoin);
                    break;
            }
        }
        else Destroy(this.gameObject);
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
            case PowerUpType.StarCoin:
                OnCollectPowerUp = StarCoin;
                break;
        }
    }

    private void StartMagnet() {
        this.transform.SetParent(null);
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(this.MagnetEffect());
    }
    private IEnumerator MagnetEffect() {
        while (true) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerData.Instance.transform.position, 5);
            foreach (Collider2D obj in hits) {
                if (obj.tag == "Coal") {
                    obj.GetComponent<Rigidbody2D>().velocity = (PlayerData.Instance.transform.position - obj.transform.position).normalized * _MagnetPullForce;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void FinishMagnet() {
        StopCoroutine(this.MagnetEffect());
        Destroy(this.gameObject);
    }

    private void StartPowderKeg() {
        DestroyObjectsAroundPlayer(areaKegEff);
        Destroy(this.gameObject);
    }

    public static void DestroyObjectsAroundPlayer(float area) {
        Collider2D[] hits = Physics2D.OverlapBoxAll(PlayerData.Instance.transform.position, new Vector2(area, 10), 0);
        foreach (Collider2D obj in hits) if (obj.tag == "Obstacle") Destroy(obj.gameObject);
    }

    private void FinishPowderKeg() {

    }

    private void StartInvincibility() {
        isPlayerInvincible = true;
        Destroy(this.gameObject);
    }

    private void FinishInvincibility() {
        isPlayerInvincible = false;
    }
    private void StartShield() {
        _shieldDamageReduction = .5f;
        Destroy(this.gameObject);
    }

    private void FinishShield() {
        _shieldDamageReduction = 1f;
    }
    private void StartCoinMultipier() {
        _coinMultiplier = 2;
        Destroy(this.gameObject);
    }

    private void FinishCoinMultiplier() {
        _coinMultiplier = 1;
    }

    private void StarCoin() {
        HudManager.Instance._currentStarCoins++;
    }
}