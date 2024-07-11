using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PowerUpHUDIcon : MonoBehaviour {

    [NonSerialized] public int ID;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _backgroundIconImage;
    [NonSerialized] public Action OnPowerUpEnd;
    private float _decreaseValue;

    private void Update() {
        ChangeDuration(-_decreaseValue * Time.deltaTime);
    }

    public void ChangeDuration(float value) {
        _fillImage.fillAmount += value;
        if (_fillImage.fillAmount <= 0f) {
            OnPowerUpEnd.Invoke();
            PowerUpHUDManager.Instance.RemovePowerUpInUI(ID);
        }
    }

    public void ResetDuration() {
        _fillImage.fillAmount = 1f;
    }

    public void PowerUpIconSetup(Sprite icon, float duration, int id, Action EndAction) {
        ID = id;
        OnPowerUpEnd = EndAction;
        _fillImage.sprite = icon;
        _backgroundIconImage.sprite = icon;
        _decreaseValue = 1f / duration;
    }
}
