using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationPanelScript : MonoBehaviour
{
    [SerializeField] private ConstellationScript _constellationScript;
    public void OpenConstellationUI(){
        this.gameObject.SetActive(false);
        ConstellationManager.Instance.ConstellationUiSetUp(_constellationScript);
    }
}
