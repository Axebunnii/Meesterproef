using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Turn : State {
    private readonly GameObject focusPoint = GameObject.Find("Player1FocusPoint");

    public Player1Turn(StateManager worldState) : base("Player1Turn", worldState) { stateMachine = worldState; }

    public override void Enter() {
        Debug.Log("start player 1 turn");
        cameraController.CameraFocus = focusPoint;
        currentPhase = Phase.PhaseStatus.draw;
        phase = new Phase();
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
