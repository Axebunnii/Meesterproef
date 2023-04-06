using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] CardManager cardManager;
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private Text winner;
    [SerializeField] private GameObject handUI;

    private int deckCount;
    [SerializeField] private Text deckCountText;

    private void Start() {
        resultsScreen.SetActive(false);

        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }
        UpdateHand();
    }

    public void ShowResults(Player player) {
        resultsScreen.SetActive(true);
        winner.text = $"{player.gameObject.name} is the winner!!!";
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateHand() {
        GameObject card;
        Sprite sprite;
        Vector3 position;

        // Reset the shown cards
        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }

        // Show cards in hand
        for (int i = 0; i < cardManager.CurrentPlayer.Hand.Count; i++) {
            card = handUI.transform.GetChild(i).gameObject;
            sprite = Resources.Load<Sprite>($"CardSprites/{cardManager.CurrentPlayer.Hand[i].CardName}");
            card.SetActive(true);
            card.GetComponent<Image>().sprite = sprite;
        }
        deckCount = cardManager.CurrentPlayer.Deck.Count;
        deckCountText.text = deckCount.ToString();
    }
}
