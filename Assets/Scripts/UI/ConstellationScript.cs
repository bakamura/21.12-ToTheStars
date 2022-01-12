using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationScript : MonoBehaviour {

    public enum PassiveTypes {
        IncreaseSpeed,
        IncreaseCoinValue
    };
    public Sprite constelationImage;
    public string constelationName;
    public PassiveTypes passiveType;
    [SerializeField] private int _starCoinCost;

    [SerializeField] private Text _constellationNameText;
    [SerializeField] private Animator _starCostAnimation;
    [System.NonSerialized] public bool isPassiveActive = false;

    private void Awake() {
        _constellationNameText.text = constelationName;
    }

    public void ActivatePassive() {
        if (!isPassiveActive) {
            isPassiveActive = true;
            switch (passiveType) {
                case PassiveTypes.IncreaseSpeed:
                    //action
                    break;
                case PassiveTypes.IncreaseCoinValue:
                    //action
                    break;
            }
        }
    }

    public void OnPressConstelationBtn() {
        if (GameManager.starCoins >= _starCoinCost) {
            GameManager.starCoins -= _starCoinCost;
            ConstellationManager.Instance.ConstellationUiSetUp(this);
            _starCostAnimation.SetTrigger("Success");
        }
        else _starCostAnimation.SetTrigger("Fail");
    }
}
