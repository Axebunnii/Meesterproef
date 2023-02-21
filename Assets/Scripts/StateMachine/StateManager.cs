using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    State currentState;

    void Start() {
        currentState = new Player1Turn(this);
        RunCurrentState();
    }

    /*void Update() {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate() {
        if (currentState != null)
            currentState.UpdatePhysics();
    }*/

    private void RunCurrentState() {
        Debug.Log("run state");
        // Enter current state if not null
        currentState?.Enter();
    }

    public void ChangeState(State newState) {
        // Exit the last state and enter the new one
        Debug.Log($"current state is {currentState}");
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}