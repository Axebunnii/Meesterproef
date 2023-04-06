using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private Text winner;

    private void Start() {
        resultsScreen.SetActive(false);
    }

    public void ShowResults(Player player) {
        resultsScreen.SetActive(true);
        winner.text = $"{player.gameObject.name} is the winner!!!";
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
