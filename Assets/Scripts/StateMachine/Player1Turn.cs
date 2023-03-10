using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Turn : State {
    public Player1Turn(StateManager worldState) : base("Player1Turn", worldState) { stateMachine = worldState; }

    public override void Enter() {
        Debug.Log("start player 1 turn");
        cameraController.currentFocus = CameraController.CameraFocus.player1;
        currentPhase = Phase.PhaseStatus.draw;
        phase = new Phase();
        // Create a new stone at the start of the turn
        GameObject prefab = Resources.Load("Projectiles/Stone") as GameObject;
        GameObject anchor = GameObject.Find("AnchorP1");
        // Make a new stone and attach it to player 1 anchor
        GameObject ins = Object.Instantiate(prefab, anchor.transform.position, Quaternion.identity);
        ins.GetComponent<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        stateMachine.State = StateManager.StateStatus.player1;

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
        Debug.Log("exit player 1 turn, enter player 2 turn");
        stateMachine.ChangeState(new Player2Turn(stateMachine));
    }
}
