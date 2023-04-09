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

    public void TakeOpeningHand(Player player) {
        currentPlayer = player;
        for (int i = 0; i < 5; i++) {
            DrawCard();
        }
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
        uiManager.UpdatePlayerUI();
    }

    public void PlayCard(int i) {
        Debug.Log(i);
        Card card = currentPlayer.Hand[i];
        // Activate card effect
        card.Use();

        // Remove the card from players hand
        currentPlayer.Hand.Remove(card);
        currentPlayer.Discard.Add(card);
        uiManager.UpdatePlayerUI();
    }

    public void DrawCardFromDeck() {
        cardDrawn = true;
    }

    public void EndCardPhasePressed() {
        endCardPhase = true;
    }

    private void PlaceInDiscard() {

    }
}
