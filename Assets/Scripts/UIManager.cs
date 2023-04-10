using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] StateManager stateManager;
    [SerializeField] CardManager cardManager;
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private Text winner;
    [SerializeField] private GameObject handUI;

    private int deckCount;
    [SerializeField] private Text deckCountText;

    [SerializeField] private Image turnDisplay;
    [SerializeField] private Text turnDisplayText;

    private int discardCount;
    [SerializeField] private Text discardCountText;

    private void Start() {
        resultsScreen.SetActive(false);

        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }
        UpdateHand();
    }

    public void UpdatePlayerUI() {
        // Update turn display
        // if player 1 turn
        turnDisplay.color = new Color(0, 0, 255);
        // if player 2 turn
        //turnDisplay.color = new Color(255, 140, 0);
        turnDisplayText.text = $"Turn {stateManager.TurnCount.ToString()}";

        // Update deck
        deckCount = cardManager.CurrentPlayer.Deck.Count;
        deckCountText.text = deckCount.ToString();
        // Update discard
        discardCount = cardManager.CurrentPlayer.Discard.Count;
        discardCountText.text = discardCount.ToString();
        // Update cards in hand
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
    }
}
