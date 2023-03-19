using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase {
    // 1. draw phase    2. card phase   3.shoot phase
    public enum PhaseStatus {draw, card, shoot};
    private Projectile projectile;
    private Player player;
    private StateManager stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
    private CardManager cardManager = GameObject.Find("GameManager").GetComponent<CardManager>();
    private Button endCardButton = GameObject.Find("Button").GetComponent<Button>();

    public void EnterDraw(State state) {
        Debug.Log("draw card");
        cardManager.EndCardPhase = false;
        endCardButton.enabled = false;
        if (stateManager.State == StateManager.StateStatus.player1) { player = GameObject.Find("Player1").GetComponent<Player>(); }
        else if (stateManager.State == StateManager.StateStatus.player2) { player = GameObject.Find("Player2").GetComponent<Player>(); }
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        projectile.CanShoot = false;
        // Wait till card has been drawn from the deck
        MonoInstance.instance.StartCoroutine(WaitForCardDrawn(state));
    }

    public void PlayCard(State state) {
        endCardButton.enabled = true;
        MonoInstance.instance.StartCoroutine(WaitForEndCardPhase(state));
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

        cardManager.CurrentPlayer = player;
        // Draw random card
        cardManager.DrawCard();

        state.CurrentPhase = PhaseStatus.card;
        MonoInstance.instance.StopCoroutine(WaitForCardDrawn(state));
        state.Update();
    }

    private IEnumerator WaitForEndCardPhase(State state) {
        yield return new WaitUntil(() => cardManager.EndCardPhase == true);
        state.CurrentPhase = PhaseStatus.shoot;
        Debug.Log("Ending card phase");
        // Activate card effect

        // Delete card from hand

        MonoInstance.instance.StopCoroutine(WaitForEndCardPhase(state));
        state.Update();
    }
}
