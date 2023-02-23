using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase{
    // 1. draw phase    2. card phase   3.shoot phase
    public enum PhaseStatus {draw, card, shoot};
    /*public enum CurrentPhase {
        get { return PhaseStatus; }
        set { PhaseStatus = value; }
    }*/

    private readonly Projectile projectile = GameObject.Find("Projectile").GetComponent<Projectile>();

    public void EnterDraw(State state) {
        Debug.Log("draw card");
        projectile.CanShoot = false;
        // Wait till card has been drawn from the deck
        MonoInstance.instance.StartCoroutine(WaitForCardDrawn(state));
    }

    private void ExitDraw(State state) {
        Debug.Log("call state update");
        state.Update();
    }

    public void PlayCard(State state) {
        Debug.Log("play card");
        MonoInstance.instance.StartCoroutine(WaitForCardPlayed(state));
    }

    public void Shoot() {
        Debug.Log("shoot");
        // Player is able to shoot
        projectile.CanShoot = true;
    }

    private IEnumerator WaitForCardDrawn(State state) {
        while (!Input.GetKeyDown(KeyCode.Space)) {
            yield return null;
        }
        Debug.Log("Key pressed: Space");
        state.CurrentPhase = PhaseStatus.card;
        MonoInstance.instance.StopCoroutine(WaitForCardDrawn(state));
        ExitDraw(state);
    }

    private IEnumerator WaitForCardPlayed(State state) {
        while (!Input.GetKeyDown(KeyCode.A)) {
            yield return null;
        }
        Debug.Log("Key pressed: A");
        state.CurrentPhase = PhaseStatus.shoot;
        // Activate card effect

        MonoInstance.instance.StopCoroutine(WaitForCardPlayed(state));
        state.Update();
    }
}
