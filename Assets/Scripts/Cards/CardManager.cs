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

    private int deckCount;
    [SerializeField] private Text deckCountText;

    private int discardCount = 0;
    [SerializeField] private Text discardCountText;

    [SerializeField] private Image turnDisplay;
    [SerializeField] private Text turnDisplayText;

    private bool endCardPhase = false;
    public bool EndCardPhase {
        get { return endCardPhase; }
        set { endCardPhase = value; }
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
    }

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
        UpdateHand();
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
        UpdateHand();
    }

    public void PlayCard() {
        Debug.Log("Play card");
    }

    public void EndCardPhasePressed() {
        Debug.Log("Pressed button");
        endCardPhase = true;
    }
}
