using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Turn : State {
    public Player2Turn(StateManager statemanager) : base("Player2Turn", statemanager) { stateManager = statemanager; }

    public override void Enter() {
        Debug.Log("start player 2 turn");
        cameraController.currentFocus = CameraController.CameraFocus.player2;
        currentPhase = Phase.PhaseStatus.draw;
        phase = new Phase();
        // Create a new stone at the start of the turn
        GameObject prefab = Resources.Load("Projectiles/Stone") as GameObject;
        GameObject anchor = GameObject.Find("AnchorP2");
        // Make a new stone and attach it to player 2 anchor
        GameObject ins = Object.Instantiate(prefab, anchor.transform.position, Quaternion.identity);
        ins.GetComponent<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        stateManager.State = StateManager.StateStatus.player2;

        Update();
    }

    public override void Update() {
        Debug.Log($"current phase: {currentPhase}");
        if (currentPhase == Phase.PhaseStatus.draw) {
            phase.EnterDraw(this);
        } else if (currentPhase == Phase.PhaseStatus.card) {
            phase.PlayCard(this);
        } else if (currentPhase == Phase.PhaseStatus.shoot) {
            phase.Shoot();
        }
    }

    public override void Exit() {
        Debug.Log("exit player 2 turn, enter player 1 turn");
        stateManager.ChangeState(new Player1Turn(stateManager));
    }
}
