using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour{
    public static UpgradeManager Instance { get; private set; }
    [SerializeField] private Text _txtCurrentCoin;
    [System.NonSerialized] public int currentCoin;

    private void Awake(){
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }

    public void ChangeCurrentCoin(int coinAmount){
        currentCoin += coinAmount;
        _txtCurrentCoin.text = currentCoin.ToString("000");
    }
}
