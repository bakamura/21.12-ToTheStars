using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //
public class Constellation {
    public bool[] stars;
}

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
        StartCoroutine(GenerateStarCoin());
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
            GameManager.starCurrency++;
            _starCoinCounterText.text = GameManager.starCurrency.ToString("000");
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

    // Reformulation

    public Constellation nameoneConstellation;
    public Constellation nametwoConstellation;
    public Constellation namethreeConstellation;
    public Constellation namefourConstellation;
    public Constellation namefiveConstellation;

    public int currentConstellation = -1;

    public void SetCurrentConstelation(int constellationN) {
        currentConstellation = constellationN;
    }

    public void SetCurrentStar(int starN) {
        CheckStarLit(GetConstellation(currentConstellation), starN);
    }

    private Constellation GetConstellation(int constellationN) {
        switch (constellationN) {
            case 0: return nameoneConstellation;
            case 1: return nametwoConstellation;
            case 2: return namethreeConstellation;
            case 3: return namefourConstellation;
            case 4: return namefiveConstellation;
            default:
                Debug.LogWarning("Error fetching constellation data");
                return null;
        }
    }

    private void CheckStarLit(Constellation constelation, int starN) {
        if (!constelation.stars[starN]) {
            if (GameManager.starCurrency > 0) {
                GameManager.starCurrency -= 1;

                int alreadyLitStars = 0;
                foreach (bool lit in constelation.stars) alreadyLitStars += lit ? 1 : 0;
                GameManager.coins += StarLitReward(alreadyLitStars);

                constelation.stars[starN] = true;
            }
            else {
                // show insuficient stars message
            }
        }
        else {
            // Do nothing ?
        }
    }

    private int StarLitReward(int i) {
        switch (i) {
            case 0: return 50;
            case 1: return 150;
            case 2: return 300;
            case 3: return 500;
            case 4: return 750;
            case 5: return 1000;
            case 6: return 1200;
            default:
                Debug.LogWarning("Reward Calc went wrong somehow");
                return 50;
        }
    }
}
