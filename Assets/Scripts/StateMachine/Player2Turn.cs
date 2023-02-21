using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Turn : State {
    public Player2Turn(StateManager worldState) : base("Player2Turn", worldState) { }

    public override void Enter() {
        Debug.Log("start player 2 turn");
    }

    public override void Update() {

    }

    public override void Exit() {
        
    }
}
