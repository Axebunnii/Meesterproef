using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {
    [SerializeField] UIManager uiManager;
    private Player currentPlayer;
    public Player CurrentPlayer {
        set { currentPlayer = value; }
        get { return currentPlayer; }
    }

    private int discardCount = 0;
    [SerializeField] private Text discardCountText;

    [SerializeField] private Image turnDisplay;
    [SerializeField] private Text turnDisplayText;

    private bool cardDrawn = false;
    public bool CardDrawn {
        get { return cardDrawn; }
        set { cardDrawn = value; }
    }

    private bool endCardPhase = false;
    public bool EndCardPhase {
        get { return endCardPhase; }
        set { endCardPhase = value; }
    }

    /*public void UpdateHand() {
        GameObject card;
        Sprite sprite;
        Vector3 position;

        // Reset the shown cards
        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }

        // Show cards in hand
        for (int i = 0; i < currentPlayer.Hand.Count; i++) {
            card = handUI.transform.GetChild(i).gameObject;
            sprite = Resources.Load<Sprite>($"CardSprites/{currentPlayer.Hand[i].CardName}");
            card.SetActive(true);
            card.GetComponent<Image>().sprite = sprite;
        }
        deckCount = currentPlayer.Deck.Count;
        deckCountText.text = deckCount.ToString();
    }*/

    public void TakeOpeningHand(Player player) {
        currentPlayer = player;
        for (int i = 0; i < 5; i++) {
            DrawCard();
        }
    }

    public void UpdatePlayerUI() {
        // if player 1 turn
        turnDisplay.GetComponent<SpriteRenderer>().color = new Color(0, 80, 255);
        // if player 2 turn
        // turnDisplay.GetComponent<SpriteRenderer>().color = new Color(255, 140, 0);
        uiManager.UpdateHand();
    }

    public void DrawCard() {
        Debug.Log($"Deck has {currentPlayer.Deck.Count} cards");
        // Get a random number thats in between 0 and the deck count
        int randomNumber = Random.Range(0, currentPlayer.Deck.Count - 1);
        Card drawnCard = currentPlayer.Deck[randomNumber];
        // Add the drawn card to the players' hand
        currentPlayer.Hand.Add(drawnCard);
        currentPlayer.Deck.Remove(drawnCard);
        Debug.Log($"Draw {drawnCard}");
        Debug.Log($"Player has {currentPlayer.Hand.Count} cards in hand");
        uiManager.UpdateHand();
    }

    public void PlayCard(int i) {
        Debug.Log(i);
        Card card = currentPlayer.Hand[i];
        // Activate card effect
        card.Use();

        // Remove the card from players hand
        currentPlayer.Hand.Remove(card);
        uiManager.UpdateHand();
    }

    public void DrawCardFromDeck() {
        cardDrawn = true;
    }

    public void EndCardPhasePressed() {
        endCardPhase = true;
    }
}
