using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State {
    private CardManager cardManager;
    private Player player1;
    private Player player2;

    public StartState(StateManager statemanager) : base("StartState", statemanager) { stateManager = statemanager; }

    public override void Enter() {
        cardManager = GameObject.Find("GameManager").GetComponent<CardManager>();
        player1 = GameObject.Find("Player 1").GetComponent<Player>();
        player2 = GameObject.Find("Player 2").GetComponent<Player>();
        Update();
    }

    public override void Update() {
        cardManager.TakeOpeningHand(player1);
        cardManager.TakeOpeningHand(player2);
        Exit();
    }

    public override void Exit() {
        stateManager.ChangeState(new Player1Turn(stateManager));
    }
}
