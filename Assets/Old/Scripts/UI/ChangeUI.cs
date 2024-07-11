using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUI : MonoBehaviour{
    private CanvasGroup _currentActiveUI = null;
    [SerializeField] private CanvasGroup _standardUI;

    public void OpenUI(CanvasGroup UI){
        if (_currentActiveUI != null)ActivateUI(_currentActiveUI, false);
        if (_currentActiveUI != UI) ActivateUI(UI, true);        
        else ActivateUI(_standardUI, true);
    }

    private void ActivateUI(CanvasGroup UI, bool active){
        UI.interactable = active;
        UI.blocksRaycasts = active;
        UI.alpha = active ? 1f : 0f;
        if (active) _currentActiveUI = UI;
    }
}
