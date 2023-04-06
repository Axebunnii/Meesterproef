using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public enum StateStatus { start, player1, player2, end };
    protected StateStatus state;
    public StateStatus State {
        set { state = value; }
        get { return state; }
    }

    private State currentState;
    public State CurrentState {
        set { currentState = value; }
        get { return currentState; }
    }


    private bool playerWon = false;
    public bool PlayerWon {
        set { playerWon = value; }
    }


    protected List<GameObject> currentProjectiles = new List<GameObject>();
    public List<GameObject> CurrentProjectiles {
        set { currentProjectiles = value; }
        get { return currentProjectiles; }
    }

    void Start() {
        currentState = new StartState(this);
        RunCurrentState();
    }

    private void RunCurrentState() {
        Debug.Log("run state");
        // Enter current state if not null
        currentState?.Enter();
    }

    public void ChangeState(State newState) {
        if (playerWon) {
            // call end state
            //currentState = new End(this);
            //currentState.Enter();
            return;
        }

        // Exit the last state and enter the new one
        Debug.Log($"current state is {currentState}");
        currentState = newState;
        currentState.Enter();
    }
}