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
    
    [Header("Components")]
    [SerializeField] private Text _constellationNameText;
    [SerializeField] private Text _starCoiCostText;
    [SerializeField] private Animator _starCostAnimation;
    [System.NonSerialized] public bool isPassiveActive = false;

    private void Awake(){
        if(_constellationNameText != null) _constellationNameText.text = constelationName;
        if(_starCoiCostText != null) _starCoiCostText.text = _starCoinCost.ToString("000");
    }

    public void ActivatePassive() {
        isPassiveActive = true;
        switch (passiveType){
            case PassiveTypes.IncreaseSpeed:
                Debug.Log("passive speed active");
                break;
            case PassiveTypes.IncreaseCoinValue:
                Debug.Log("passive coinvalue active");
                break;
        }
    }

    public void OnPressConstellationBtn() {
        if (!isPassiveActive){
            if (GameManager.starCurrency >= _starCoinCost){
                GameManager.starCurrency -= _starCoinCost;
                ActivatePassive();
                _starCostAnimation.SetTrigger("Success");
            }
            else _starCostAnimation.SetTrigger("Fail");
        }
        //else ConstellationManager.Instance.ConstellationUiSetUp(this);       
    }
}
