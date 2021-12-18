using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; } = null;

    public float maxHealth;
    public float currentHealth;
    public float healthLoss;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        currentHealth = maxHealth * 0.75f;
    }

    private void Update() {
        currentHealth -= healthLoss * Time.deltaTime;
        if (currentHealth <= 0) Die();
    }

    public void ChangeHealth(float changeAmount) {
        currentHealth += changeAmount;
    }

    private void Die() {
        Debug.Log("Player Died");
        this.enabled = false;
    }
}
