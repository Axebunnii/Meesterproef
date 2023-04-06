using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : State {
    private UIManager uiManager;

    public End(StateManager statemanager) : base("End", statemanager) { 
        stateManager = statemanager;
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

    public override void Enter() {
        Update();
    }

    public override void Update() {
        // Show results
        //uiManager.ShowResults();
        // Exit
        Exit();
    }

    public override void Exit() {
        // go back to main menu
    }
}