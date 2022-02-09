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

    [NonSerialized] public static bool burnObstacles = false;
    [NonSerialized] public static int extraLife = 0;
    public static int baseScoreIncrease = 10;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        animPlayer = GetComponent<Animator>();
        ChangeHealth((float)(maxHealth * (0.5f + 0.05 * GameManager.playerUpgrades[1])));
    }

    private void Update() {
        if (currentHealth > 0)
        {
            HudManager.Instance.ChangeScore(baseScoreIncrease * MapGenerator.Instance.VelocityCalc()); //
            if (!PowerUp.isPlayerInvincible) ChangeHealth(-healthLoss * MapGenerator.Instance.VelocityCalc());
        }
        else Die();
    }

    public void ChangeHealth(float changeAmount) {
        if (currentHealth < changeAmount && currentHealth >= maxHealth * .8f) {
            Debug.Log("Revived");
            //animation + sound feedback
            extraLife--;
            return;
        }

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
        HudManager.Instance.ResultScreen();/**/
        PlayerMovement.Instance.enabled = false;
        MapGenerator.Instance.isMoving = false;
        PowerUpHUDManager.Instance.ClearUI();
        this.enabled = false; //
    }

    public void RestartPlayer(){
        this.enabled = true; //
        PlayerMovement.Instance.enabled = true;
        currentHealth = (float)(maxHealth * (0.5f + 0.05 * GameManager.playerUpgrades[1]));
        animPlayer.SetTrigger("Restart");
    }
}
