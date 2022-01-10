using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance { get; private set; } = null;

    private Animator animPlayer;

    public float maxHealth;
    [NonSerialized] public float currentHealth;
    public float healthLoss;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        animPlayer = GetComponent<Animator>();
        ChangeHealth(maxHealth * 0.75f);
    }

    private void Update() {
       if(!PowerUp.isPlayerInvincible)
            ChangeHealth(-healthLoss * Time.deltaTime);
        if (currentHealth <= 0) Die();
    }

    public void ChangeHealth(float changeAmount) {
        currentHealth += changeAmount;
        if (currentHealth < 0) {
            if (changeAmount <= -500) animPlayer.SetTrigger("DeathInsta");
            else animPlayer.SetTrigger("Death");
        }
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
