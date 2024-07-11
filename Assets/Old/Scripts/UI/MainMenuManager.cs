using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public CanvasGroup[] menus;

    public void OpenMenuBtn(CanvasGroup canvasOpen) {
        for (int i = 0; i < menus.Length; i++) HudManager.ActivateHudElement(menus[i], menus[i] == canvasOpen);
    }

    public void PlayBtn() {
        SceneManager.LoadScene(1);
        // Make constellations and upgrades change game values.
    }

    public void TryBuyUpgrade(int upgrade /* make enum */) {
        // Stuff
    }

}
