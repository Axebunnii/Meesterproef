using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Turn : State {
    public Player2Turn(StateManager worldState) : base("Player2Turn", worldState) { stateMachine = worldState; }

    public override void Enter() {
        Debug.Log("start player 2 turn");
        cameraController.currentFocus = CameraController.CameraFocus.player2;
        currentPhase = Phase.PhaseStatus.draw;
        phase = new Phase();
        Update();
    }

    public override void Update() {
        Debug.Log($"current phase: {currentPhase}");
        if (currentPhase == Phase.PhaseStatus.draw) {
            phase.EnterDraw(this);
        }
        else if (currentPhase == Phase.PhaseStatus.card) {
            phase.PlayCard(this);
        }
        else if (currentPhase == Phase.PhaseStatus.shoot) {
            phase.Shoot();
        }
    }

    public override void Exit() {
        Debug.Log("exit player 1 turn, enter player 2 turn");
        stateMachine.ChangeState(new Player1Turn(stateMachine));
    }
}
