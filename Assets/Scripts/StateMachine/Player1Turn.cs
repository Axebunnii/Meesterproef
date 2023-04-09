using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Turn : State {
    public Player1Turn(StateManager sm) : base("Player1Turn", sm) {
        stateManager = sm;
        cardManager = GameObject.Find("GameManager").GetComponent<CardManager>();
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

    public override void Enter() {
        // Set the player in de cardmanager and update the ui to the right player
        cardManager.CurrentPlayer = GameObject.Find("Player 1").GetComponent<Player>();
        uiManager.UpdatePlayerUI();
        // Set focus of camera
        cameraController.currentFocus = CameraController.CameraFocus.player1;
        currentPhase = Phase.PhaseStatus.draw;
        phase = new Phase();
        // Create a new stone at the start of the turn
        GameObject prefab = Resources.Load("Projectiles/Stone") as GameObject;
        GameObject anchor = GameObject.Find("AnchorP1");
        // Make a new stone and attach it to player 1 anchor
        GameObject ins = Object.Instantiate(prefab, anchor.transform.position, Quaternion.identity);
        ins.GetComponent<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
        stateManager.CurrentProjectiles.Add(ins.gameObject);
        projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        stateManager.State = StateManager.StateStatus.player1;

        Update();
    }

    public override void Exit() {
        Debug.Log("exit player 1 turn, enter player 2 turn");
        Debug.Log(stateManager.CurrentProjectiles.Count);
        if (stateManager.CurrentProjectiles.Count == 0)
            stateManager.ChangeState(new Player2Turn(stateManager));
    }
}
