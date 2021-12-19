using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; } = null;

    public float maxHealth;
    private float _currentHealth;
    public float healthLoss;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        ChangeHealth(maxHealth * 0.75f);
    }

    private void Update() {
        ChangeHealth(-healthLoss * Time.deltaTime);
        if (_currentHealth <= 0) Die();
    }

    public void ChangeHealth(float changeAmount) {
        _currentHealth += changeAmount;
        HudManager.Instance.ChangeHealthBarFill(_currentHealth / maxHealth);
    }

    private void Die() {
        Debug.Log("Player Died"); //
        this.enabled = false; //
    }
}
