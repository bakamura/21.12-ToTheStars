using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHUDManager : MonoBehaviour {
    public static PowerUpHUDManager Instance{ get; private set; }
    private const int _divider = 5;
    [SerializeField] private GameObject powerUpIconPrefab;
    private float _iconWidith;
    private float _iconHeight;
    private Dictionary<int, PowerUpHUDIconScript> _iconsInScene = new Dictionary<int, PowerUpHUDIconScript>();
    private List<RectTransform> _iconsPositions = new List<RectTransform>();
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            _iconWidith = powerUpIconPrefab.GetComponent<RectTransform>().rect.width;
            _iconHeight = powerUpIconPrefab.GetComponent<RectTransform>().rect.height;
        }
    }
    public void CreatePowerUpInUI(PowerUpScript _powerUpActivated) {
        if (!_iconsInScene.ContainsKey((int)_powerUpActivated._powerUpType)){
            GameObject icon = Instantiate(powerUpIconPrefab, transform.position, Quaternion.identity, transform);
            icon.GetComponent<PowerUpHUDIconScript>().PowerUpIconSetup(_powerUpActivated._icon.sprite, _powerUpActivated._powerUpDuration, (int)_powerUpActivated._powerUpType, _powerUpActivated.EndAction);
            _iconsInScene.Add((int)_powerUpActivated._powerUpType, icon.GetComponent<PowerUpHUDIconScript>());
            _iconsPositions.Add(icon.GetComponent<RectTransform>());
        }
        else {
            _iconsInScene[(int)_powerUpActivated._powerUpType].ResetDuration();
        }
        UpdateIconsPositions();
    }
    public void RemovePowerUpInUI(PowerUpHUDIconScript _powerUpFinished) {
        if (_iconsInScene.ContainsKey(_powerUpFinished.ID)){
            Destroy(_iconsInScene[_powerUpFinished.ID].gameObject);
            _iconsInScene.Remove(_powerUpFinished.ID);
            _iconsPositions.Remove(_powerUpFinished.GetComponent<RectTransform>());
            UpdateIconsPositions();
        }
    }
    private void UpdateIconsPositions() {
        for(int currentIcon = 0; currentIcon < _iconsInScene.Count; currentIcon++){
            _iconsPositions[currentIcon].transform.localPosition = new Vector2(currentIcon % _divider * _iconWidith, -currentIcon / _divider * _iconHeight);
        }
    }
}
