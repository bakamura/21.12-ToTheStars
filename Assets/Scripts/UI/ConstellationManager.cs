using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationManager : MonoBehaviour {

    public static ConstellationManager Instance { get; private set; }

    private static float _currentStarCoinProgress; // needs to be saved
    private float _requiredProgressForNewStarCoin = 1; // needs to be saved
    private int _totalStarCoinsGeneretaed; //needs to be saved

    [SerializeField] private Text _starCoinCounterText;
    [SerializeField] private GameObject _constellationHallUI;
    [SerializeField] private GameObject _constelationUI;
    [SerializeField] private Text _constelationName;
    [SerializeField] private Image _constelationImage;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }

    private void Start() {
        StartCoroutine(this.GenerateStarCoin());
    }

    IEnumerator GenerateStarCoin() {
        while (MapGenerator.Instance.isMoving) {
            _currentStarCoinProgress += MapGenerator.Instance.VelocityCalc();
            CheckForNewStarCoin();
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    private void CheckForNewStarCoin() {
        if (_currentStarCoinProgress >= _requiredProgressForNewStarCoin) {
            _totalStarCoinsGeneretaed++;
            GameManager.starCoins++;
            _starCoinCounterText.text = GameManager.starCoins.ToString("000");
            _requiredProgressForNewStarCoin = (_totalStarCoinsGeneretaed + 1) * 1;
            _currentStarCoinProgress = 0;
        }
    }
    public void ConstellationUiSetUp(ConstellationScript constellationScript) {
        _constelationImage.sprite = constellationScript.constelationImage;
        _constelationName.text = constellationScript.constelationName;
        Open_CloseConstellationHallUI();//closes hall UI
        Open_CloseConstellationUI();// opens constellation UI
    }

    public void Open_CloseConstellationHallUI() {
        _constellationHallUI.SetActive(!_constellationHallUI.activeSelf);
    }

    public void Open_CloseConstellationUI() {
        _constelationUI.SetActive(!_constelationUI.activeSelf);
    }
}
