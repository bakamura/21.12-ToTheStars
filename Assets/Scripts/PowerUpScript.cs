using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    private const int _totalPowerUps = 2;
    public enum PowerUpType{
        PowerUp1,
        PowerUp2
    };
    public PowerUpType _powerUpType;
    [SerializeField] private Sprite[] _iconsList = new Sprite[_totalPowerUps];
    [SerializeField] private float[] _durationsList = new float[_totalPowerUps];
    private Action OnCollectPowerUp;
    [NonSerialized] public Action EndAction;
    [NonSerialized] public SpriteRenderer _icon;
    [NonSerialized] public float _powerUpDuration;
    // Start is called before the first frame update
    private void Awake() {
        _icon = GetComponent<SpriteRenderer>();
        PowerUpSetup();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player"){
            PowerUpHUDManager.Instance.CreatePowerUpInUI(this);
            OnCollectPowerUp.Invoke();
            Destroy(this.gameObject);        
        }
    }
    public void PowerUpSetup(){
        RandomizePowerUpType();
        _icon.sprite = _iconsList[(int)_powerUpType];
        _powerUpDuration = _durationsList[(int)_powerUpType];
        SetPowerUpStartAction();
    }
    private void RandomizePowerUpType(){
        int r = UnityEngine.Random.Range(0, _totalPowerUps);
        switch(r){
            case (0):
                _powerUpType = PowerUpType.PowerUp1;
                break;
            case (1):
                _powerUpType = PowerUpType.PowerUp2;
                break;
        }
    }
    private void SetPowerUpStartAction() {
        switch (_powerUpType) {
            case PowerUpType.PowerUp1:
                OnCollectPowerUp = StartPowerUp1;
                EndAction = FinishPowerUp1;
                break;
            case PowerUpType.PowerUp2:
                OnCollectPowerUp = StartPowerUp2;
                EndAction = FinishPowerUp2;
                break;
        }
    }
    private void StartPowerUp1()
    {
        Debug.Log("PowerUP1");
    }
    private void StartPowerUp2()
    {
        Debug.Log("PowerUP2");
    }
    private void FinishPowerUp1()
    {
        Debug.Log("FinishPowerUP1");
    }
    private void FinishPowerUp2()
    {
        Debug.Log("FinishPowerUP2");
    }
}
