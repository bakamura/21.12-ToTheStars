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
    //[SerializeField] private float _powderKegSpeedIncrease;
    //[NonSerialized] public static float maxSizePlayerIncreasePowderKeg = 3;
    [SerializeField] private float _MagnetPullForce;
    [NonSerialized] public static float _shieldDamageReduction = 0;
    [NonSerialized] public static int _coinMultiplier = 1;
    [NonSerialized] public static bool isPlayerInvincible = false;
    [NonSerialized] public static bool isPlayerFlying = false;
    //[NonSerialized] public Coroutine _changeInSizeCoroutine = null;
    //private Vector2 _playerSizeBeforeChange;

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
        this.transform.SetParent(null);
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(this.MagnetEffect());
    }
    private IEnumerator MagnetEffect(){
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerData.Instance.transform.position, 5);
            foreach(Collider2D obj in hits) {
                if(obj.tag == "Coal"){
                    obj.GetComponent<Rigidbody2D>().velocity = (PlayerData.Instance.transform.position - obj.transform.position).normalized * _MagnetPullForce;
                }
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    private void FinishMagnet() {
        StopCoroutine(this.MagnetEffect());
        Destroy(this.gameObject);
    }

    private void StartPowderKeg() {
        Collider2D[] hits = Physics2D.OverlapBoxAll(PlayerData.Instance.transform.position, new Vector2(areaKegEff, 10), 0);
        foreach (Collider2D obj in hits) if (obj.tag == "Obstacle") Destroy(obj.gameObject);
        Destroy(this.gameObject);
        //a way to make the canon upgrade
        //PlayerData.Instance.isFlying = true;
        //MapGenerator.Instance._powderKegSpeedIncrease = _powderKegSpeedIncrease;
        //PlayerMovement.Instance.ChangeSize(true);
    }

    //public void ChangeSize(bool increaseSize)
    //{
    //    if (_changeInSizeCoroutine == null)
    //    {
    //        _changeInSizeCoroutine = StartCoroutine(this.ChangeSizeEffectPowderKeg(increaseSize));
    //    }
    //}

    //private IEnumerator ChangeSizeEffectPowderKeg(bool increaseSize)
    //{
    //    Vector2 changeInScale = Vector2.zero;
    //    if (increaseSize)
    //    {
    //        _playerSizeBeforeChange = new Vector2(PlayerData.Instance.transform.localScale.x, PlayerData.Instance.transform.localScale.y);
    //        changeInScale = _playerSizeBeforeChange;
    //        while (transform.localScale.x < PowerUp.maxSizePlayerIncreasePowderKeg && transform.localScale.y < PowerUp.maxSizePlayerIncreasePowderKeg)
    //        {
    //            changeInScale += new Vector2(.05f, .05f);
    //            transform.localScale = changeInScale;
    //            yield return new WaitForSeconds(Time.fixedDeltaTime);
    //        }
    //        _changeInSizeCoroutine = null;
    //    }
    //    else
    //    {
    //        changeInScale = PlayerData.Instance.transform.localScale;
    //        while (transform.localScale.x > _playerSizeBeforeChange.x && transform.localScale.y > _playerSizeBeforeChange.y)
    //        {
    //            changeInScale -= new Vector2(.1f, .1f);
    //            transform.localScale = changeInScale;
    //            yield return new WaitForSeconds(Time.fixedDeltaTime);
    //        }
    //        _changeInSizeCoroutine = null;
    //    }
    //}

    private void FinishPowderKeg() {
        //a way to make the canon upgrade
        //PlayerMovement.Instance.ChangeSize(false);
        //MapGenerator.Instance._powderKegSpeedIncrease = 0f;
        //PlayerData.Instance.isFlying = false;
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
        _shieldDamageReduction = 0;
    }
    private void StartCoinMultipier() {
        _coinMultiplier = 2;
        Destroy(this.gameObject);
    }

    private void FinishCoinMultiplier() {
        _coinMultiplier = 1;
    }
}