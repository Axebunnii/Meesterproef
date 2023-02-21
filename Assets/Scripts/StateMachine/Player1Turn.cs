using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Turn : State {
    public Player1Turn(StateManager worldState) : base("Player1Turn", worldState) { }

    public override void Enter() {
        Debug.Log("start player 1 turn");
        //Exit();
        Update();
    }

    public override void Update() {
        phase = new Phase();
        if (phase.CurrentPhase == "Draw") {
            phase.Draw();
        } else if (phase.CurrentPhase == "Play Card") {
            phase.PlayCard();
            phase.CurrentPhase = "Play Card";
        } else if (phase.CurrentPhase == "Shoot") {
            phase.Shoot();
        }
    }

    public override void Exit() {
        stateMachine.ChangeState(new Player2Turn(stateMachine));
    }
}
