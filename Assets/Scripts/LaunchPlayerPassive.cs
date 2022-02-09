using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayerPassive : MonoBehaviour {
    public static LaunchPlayerPassive Instance { get; private set; }

    [SerializeField] private float _speedIncrease;
    [SerializeField] private int _scoreGainIncrease;
    private const float maxSizePlayerIncreasePowderKeg = 5;
    private Vector2 _playerSizeBeforeChange;
    [NonSerialized] public static bool isPlayerFlying = false;
    [NonSerialized] public static bool isPassiveActive = true;
    // Start is called before the first frame update
    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this); 
    }
    void Start() {
        Activate();
    }
    public void Activate() {
        if (isPassiveActive) {
            isPlayerFlying = true;
            MapGenerator.baseSpeed += _speedIncrease;
            PlayerData.baseScoreIncrease += _scoreGainIncrease;
            StartCoroutine(this.ChangeSizeEffectPowderKeg());
        }
    }

    private IEnumerator ChangeSizeEffectPowderKeg() {
        Vector2 changeInScale = Vector2.zero;
        _playerSizeBeforeChange = new Vector2(PlayerData.Instance.transform.localScale.x, PlayerData.Instance.transform.localScale.y);
        changeInScale = _playerSizeBeforeChange;
        while (PlayerData.Instance.transform.localScale.x < maxSizePlayerIncreasePowderKeg && PlayerData.Instance.transform.localScale.y < maxSizePlayerIncreasePowderKeg) {
            changeInScale += new Vector2(.05f, .05f);
            PlayerData.Instance.transform.localScale = changeInScale;
            yield return new WaitForFixedUpdate();
        }
        //changeInScale = PlayerData.Instance.transform.localScale;
        //return to normal size
        while (PlayerData.Instance.transform.localScale.x > _playerSizeBeforeChange.x && PlayerData.Instance.transform.localScale.y > _playerSizeBeforeChange.y) {
            changeInScale -= new Vector2(.1f, .1f);
            PlayerData.Instance.transform.localScale = changeInScale;
            yield return new WaitForFixedUpdate();
        }
        //return to normal speed
        MapGenerator.baseSpeed -= _speedIncrease;
        PlayerData.baseScoreIncrease -= _scoreGainIncrease;
        isPlayerFlying = false;
        PowerUp.DestroyObjectsAroundPlayer(10f);
    }
}
