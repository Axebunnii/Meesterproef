using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu {    
    public void StartGame() {
        SceneManager.LoadScene("Level");
    }

    public void GoToSettings() {
        mainmenu.SetActive(false);
        settingsmenu.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
