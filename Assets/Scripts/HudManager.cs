using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    public static HudManager Instance { get; private set; } = null;

    [SerializeField] private Image _heathBarFill;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void ChangeHealthBarFill(float fillAmount) {
        _heathBarFill.fillAmount = fillAmount;
    }
}
