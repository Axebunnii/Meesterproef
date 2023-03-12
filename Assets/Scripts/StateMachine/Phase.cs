using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase {
    // 1. draw phase    2. card phase   3.shoot phase
    public enum PhaseStatus {draw, card, shoot};
    private Projectile projectile;
    private Player player;
    private StateManager stateManager;
    private CardManager cardManager;

    public void EnterDraw(State state) {
        Debug.Log("draw card");
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        cardManager = GameObject.Find("GameManager").GetComponent<CardManager>();
        if (stateManager.State == StateManager.StateStatus.player1) { player = GameObject.Find("Player1").GetComponent<Player>(); }
        else if (stateManager.State == StateManager.StateStatus.player2) { player = GameObject.Find("Player2").GetComponent<Player>(); }
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        projectile.CanShoot = false;
        // Wait till card has been drawn from the deck
        MonoInstance.instance.StartCoroutine(WaitForCardDrawn(state));
    }

    private void ExitDraw(State state) {
        state.Update();
    }

    public void PlayCard(State state) {
        MonoInstance.instance.StartCoroutine(WaitForCardPlayed(state));
    }

    public void Shoot() {
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        // Player is able to shoot
        projectile.CanShoot = true;
    }

    private IEnumerator WaitForCardDrawn(State state) {
        while (!Input.GetKeyDown(KeyCode.Space)) {
            yield return null;
        }
        // Draw card
        if (player.Deck.Count < 1) {
            // Player losses game

            yield return null;
        }
        // Draw random card
        int randomNumber = Random.Range(0, player.Deck.Count - 1);
        Card drawnCard = player.Deck[randomNumber];
        player.Hand.Add(drawnCard);
        Debug.Log($"Draw {drawnCard}");
        Debug.Log($"Player has {player.Hand.Count} cards in hand");
        cardManager.UpdateHand();

        state.CurrentPhase = PhaseStatus.card;
        MonoInstance.instance.StopCoroutine(WaitForCardDrawn(state));
        ExitDraw(state);
    }

    private IEnumerator WaitForCardPlayed(State state) {
        while (!Input.GetKeyDown(KeyCode.A)) {
            yield return null;
        }
        state.CurrentPhase = PhaseStatus.shoot;
        // Activate card effect

        MonoInstance.instance.StopCoroutine(WaitForCardPlayed(state));
        state.Update();
    }
}
