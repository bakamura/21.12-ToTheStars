using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    public CanvasGroup mainMenu;
    public CanvasGroup upgradeMenu;
    public CanvasGroup constellationMenu;
    public CanvasGroup settingsMenu;

    public void OpenUpgradeMenu() {
        HudManager.ActivateHudElement(upgradeMenu, true);
        HudManager.ActivateHudElement(mainMenu, false);
    }

    public void OpenConstellationMenu() {
        HudManager.ActivateHudElement(constellationMenu, true);
        HudManager.ActivateHudElement(mainMenu, false);
    }

    public void OpenSettingsMenu() {
        HudManager.ActivateHudElement(settingsMenu, true);
        HudManager.ActivateHudElement(mainMenu, false);
    }

    public void CloseMenu() {
        HudManager.ActivateHudElement(mainMenu, true);
        HudManager.ActivateHudElement(upgradeMenu, false);
        HudManager.ActivateHudElement(constellationMenu, false);
        HudManager.ActivateHudElement(settingsMenu, false);
    }

}
