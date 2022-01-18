using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour {

    public static HudManager Instance { get; private set; } = null;

    [SerializeField] private Image _heathBarFill;

    private float _currentScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void ChangeHealthBarFill(float fillAmount) {
        _heathBarFill.fillAmount = fillAmount;
    }

    public void ChangeScore(float amount) {
        _currentScore += amount;
        _scoreText.text = _currentScore.ToString("F0");
    }
}
