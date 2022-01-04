using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; } = null;

    public float maxHealth;
    [NonSerialized] public float currentHealth;
    public float healthLoss;
    public bool isInvincible;
    public bool isFlying;

    public GameObject magneticEffect;
    public GameObject powderKegEffect;
    [NonSerialized] public float shieldDamageReduction;
    [NonSerialized] public int coinMultiplierPowerUp = 1;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        ChangeHealth(maxHealth * 0.75f);
    }

    private void Update() {
        if (!isInvincible)
            ChangeHealth(-healthLoss * Time.deltaTime);
        if (currentHealth <= 0) Die();
    }

    public void ChangeHealth(float changeAmount) {
        currentHealth += changeAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        HudManager.Instance.ChangeHealthBarFill(currentHealth / maxHealth);
    }

    private void Die() {
        Debug.Log("Player Died"); //
        PlayerMovement.Instance.enabled = false;
        MapGenerator.Instance.isMoving = false;
        PowerUpHUDManager.Instance.ClearUI();
        this.enabled = false; //
    }
}
