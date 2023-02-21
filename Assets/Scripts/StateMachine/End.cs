using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : State {
    public End(StateManager worldState) : base("End", worldState) { }

    public override void Enter() {
        Debug.Log("Player has been defeated");
        Update();
    }

    public override void Update() {
        // Show results

        // Exit
        Exit();
    }

    public override void Exit() {
        // go back to main menu
    }
}