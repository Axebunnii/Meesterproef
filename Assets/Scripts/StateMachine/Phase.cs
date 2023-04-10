using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Phase {
    // 1. draw phase    2. card phase   3.shoot phase
    public enum PhaseStatus {draw, card, shoot};
    private Projectile projectile;
    private StateManager stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
    private CardManager cardManager = GameObject.Find("GameManager").GetComponent<CardManager>();
    private readonly Button deckButton = GameObject.Find("Deck").GetComponent<Button>();
    private readonly Button endCardButton = GameObject.Find("Button").GetComponent<Button>();

    public void EnterDraw(State state) {
        cardManager.CardDrawn = false;
        cardManager.EndCardPhase = false;
        // Make the deck clickable
        deckButton.enabled = true;
        // Make button unclickable
        endCardButton.enabled = false;
        // Make cards unclickable
        foreach (Transform card in GameObject.Find("Hand").transform) {
            card.gameObject.GetComponent<EventTrigger>().enabled = false;
        }
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        projectile.CanShoot = false;
        // Wait till card has been drawn from the deck
        MonoInstance.instance.StartCoroutine(WaitForCardDrawn(state));
    }

    public void PlayCard(State state) {
        // Make deck unclickable
        deckButton.enabled = false;
        // Make button clickable
        endCardButton.enabled = true;
        // Make cards clickable
        foreach (Transform card in GameObject.Find("Hand").transform) {
            card.gameObject.GetComponent<EventTrigger>().enabled = true;
        }
        MonoInstance.instance.StartCoroutine(WaitForEndCardPhase(state));
    }

    public void Shoot() {
        // Make button unclickable
        endCardButton.enabled = false;
        // Make cards unclickable
        foreach (Transform card in GameObject.Find("Hand").transform) {
            card.gameObject.GetComponent<EventTrigger>().enabled = false;
        }
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        // Player is able to shoot
        for (int i = 0; i < stateManager.CurrentProjectiles.Count; i++) {
            Projectile p = null;
            p = stateManager.CurrentProjectiles[i].GetComponent<StoneProjectile>();
            if (p == null)
                p = stateManager.CurrentProjectiles[i].GetComponent<BombProjectile>();
            p.CanShoot = true;
        }
    }

    private IEnumerator WaitForCardDrawn(State state) {
        yield return new WaitUntil(() => cardManager.CardDrawn == true);
        // Draw card
        if (cardManager.CurrentPlayer.Deck.Count == 0) {
            // Player losses game
            cardManager.CurrentPlayer.CheckCondetions();
            yield break;
        }

        // Draw random card
        cardManager.DrawCard();

        state.CurrentPhase = PhaseStatus.card;
        MonoInstance.instance.StopCoroutine(WaitForCardDrawn(state));
        state.Update();
    }

    private IEnumerator WaitForEndCardPhase(State state) {
        yield return new WaitUntil(() => cardManager.EndCardPhase == true);
        state.CurrentPhase = PhaseStatus.shoot;
        // Activate card effect

        // Delete card from hand

        MonoInstance.instance.StopCoroutine(WaitForEndCardPhase(state));
        state.Update();
    }
}
