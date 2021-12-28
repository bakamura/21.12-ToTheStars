﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHUDManager : MonoBehaviour {

    public static PowerUpHUDManager Instance { get; private set; }

    // Number of icons in a same line.
    private const int _divider = 5;
    [SerializeField] private GameObject _powerUpIconPrefab;
    private float _iconWidith;
    private float _iconHeight;
    // As the key is an int, it works basicaly as a list (?)
    private Dictionary<int, PowerUpHUDIcon> _iconsInScene = new Dictionary<int, PowerUpHUDIcon>();
    private List<RectTransform> _iconsPositions = new List<RectTransform>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            _iconWidith = _powerUpIconPrefab.GetComponent<RectTransform>().rect.width;
            _iconHeight = _powerUpIconPrefab.GetComponent<RectTransform>().rect.height;
        }
        else if (Instance != this) Destroy(this.gameObject);
    }

    public bool CreatePowerUpInUI(PowerUp _powerUpActivated) {
        if (!_iconsInScene.ContainsKey((int)_powerUpActivated._powerUpType)) {
            GameObject icon = Instantiate(_powerUpIconPrefab, transform.position, Quaternion.identity, transform);
            icon.GetComponent<PowerUpHUDIcon>().PowerUpIconSetup(_powerUpActivated.srPowerUp.sprite, _powerUpActivated.powerUpDuration, (int)_powerUpActivated._powerUpType, _powerUpActivated.OnPowerUpExpires);

            _iconsInScene.Add((int)_powerUpActivated._powerUpType, icon.GetComponent<PowerUpHUDIcon>());
            _iconsPositions.Add(icon.GetComponent<RectTransform>());
            UpdateIconsPositions();
            return true;
        }
        else {
            _iconsInScene[(int)_powerUpActivated._powerUpType].ResetDuration();
            return false;
        }
    }

    public void RemovePowerUpInUI(PowerUpHUDIcon _powerUpFinished) {
        if (_iconsInScene.ContainsKey(_powerUpFinished.ID)) {
            Destroy(_iconsInScene[_powerUpFinished.ID].gameObject);
            _iconsInScene.Remove(_powerUpFinished.ID);
            _iconsPositions.Remove(_powerUpFinished.GetComponent<RectTransform>());
            UpdateIconsPositions();
        }
        else Debug.Log("An error occurred whilist fetching ID");
    }

    private void UpdateIconsPositions() {
        // Probably won't see use, but it's useful if it's ever needed.
        for (int currentIcon = 0; currentIcon < _iconsInScene.Count; currentIcon++) {
            _iconsPositions[currentIcon].transform.localPosition = new Vector2(currentIcon % _divider * _iconWidith, -currentIcon / _divider * _iconHeight);
        }
    }
}
