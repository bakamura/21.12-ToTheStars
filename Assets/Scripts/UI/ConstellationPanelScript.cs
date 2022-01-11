using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationPanelScript : MonoBehaviour
{
    public void OpenConstelationUI(){
        ConstellationManager.Instance.Open_CloseConstelationUIBtn();
        this.gameObject.SetActive(false);
    }
}
