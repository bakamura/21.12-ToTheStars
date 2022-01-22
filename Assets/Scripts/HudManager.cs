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
    private int _currentRunCoins = 0;
    [SerializeField] private TextMeshProUGUI _coinsText;

    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Sprite[] _pauseBtnSprites = new Sprite[2]; // 0 is pause, 1 is resume
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private CanvasGroup _settingsBtn;
    [SerializeField] private CanvasGroup _homeBtn;
    [SerializeField] private CanvasGroup _settingsMenu;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        ChangeRunCoins(0);
        ChangeScore(0);

        ActivateHudElement(_pauseText.GetComponent<CanvasGroup>(), false);
        ActivateHudElement(_settingsMenu.GetComponent<CanvasGroup>(), false);
    }

    public void ChangeHealthBarFill(float fillAmount) {
        _heathBarFill.fillAmount = fillAmount;
    }

    public void ChangeScore(float amount) {
        _currentScore += amount;
        _scoreText.text = _currentScore.ToString("F0");
    }

    public void ChangeRunCoins(int amount) {
        _currentRunCoins += amount;
        _coinsText.text = _currentRunCoins.ToString("F0");
    }

    public void PauseMenuBtn() {
        if (!_pauseText.GetComponent<CanvasGroup>().interactable) {
            Time.timeScale = 0;

            _pauseBtn.image.sprite = _pauseBtnSprites[1];
            ActivateHudElement(_pauseText.GetComponent<CanvasGroup>(), true);
            ActivateHudElement(_settingsBtn.GetComponent<CanvasGroup>(), true);
            ActivateHudElement(_homeBtn.GetComponent<CanvasGroup>(), true);
        }
        else {
            StartCoroutine(ResumeGame());

            _pauseBtn.image.sprite = _pauseBtnSprites[0];
        }
    }

    private IEnumerator ResumeGame() {
        ActivateHudElement(_settingsBtn.GetComponent<CanvasGroup>(), false);
        ActivateHudElement(_homeBtn.GetComponent<CanvasGroup>(), false);
        _pauseText.text = "3";

        yield return new WaitForSecondsRealtime(0.33f);

        _pauseText.text = "2";
        
        yield return new WaitForSecondsRealtime(0.33f);

        _pauseText.text = "1";

        yield return new WaitForSecondsRealtime(0.33f);

        ActivateHudElement(_pauseText.GetComponent<CanvasGroup>(), false);
        _pauseText.text = "-Paused-";

        yield return new WaitForSecondsRealtime(0.01f);

        Time.timeScale = 1;
    }

    public void ReturnToMainMenu() {
        // Return to the main menu
    }

    public void SettingsBtn(bool active) {
        ActivateHudElement(_settingsMenu, active);
    }

    public void MusicSlider(float volume) {
        // Change vol based on "volume" input
    }

    public void FxSlider(float volume) {
        // Change vol based on "volume" input
    }

    public static void ActivateHudElement(CanvasGroup canvas, bool active) {
        canvas.alpha = active ? 1 : 0;
        canvas.interactable = active;
        canvas.blocksRaycasts = active;
    }


}
