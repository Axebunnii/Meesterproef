using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {
    [SerializeField] private GameObject handUI;
    private Player currentPlayer;
    public Player CurrentPlayer {
        set { currentPlayer = value; }
    }

    private void Start() {
        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }
        UpdateHand();
    }

    public void UpdateHand() {
        GameObject card;
        Sprite sprite;

        // Reset the shown cards
        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }

        // Show cards in hand
        for (int i = 0; i < currentPlayer.Hand.Count; i++) {
            card = handUI.transform.GetChild(i).gameObject;
            sprite = Resources.Load<Sprite>($"Cards/{currentPlayer.Hand[i].CardName}");
            card.SetActive(true);
            card.GetComponent<Image>().sprite = sprite;
        }
    }

    public void TakeOpeningHand(Player player) {
        currentPlayer = player;
        for (int i = 0; i < 5; i++) {
            DrawCard();
        }
    }

    public void ShowPlayerHand() {
        UpdateHand();
    }

    public void DrawCard() {
        Debug.Log($"Deck has {currentPlayer.Deck.Count} cards");
        // Get a random number thats in between 0 and the deck count
        int randomNumber = Random.Range(0, currentPlayer.Deck.Count - 1);
        Card drawnCard = currentPlayer.Deck[randomNumber];
        // Add the drawn card to the players' hand
        currentPlayer.Hand.Add(drawnCard);
        Debug.Log($"Draw {drawnCard}");
        Debug.Log($"Player has {currentPlayer.Hand.Count} cards in hand");
        UpdateHand();
    }
}
