using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State{
    public string name;
    protected StateManager stateMachine;
    protected Phase phase;
    protected Projectile projectile;
    protected MonoBehaviour monoBehaviour;
    protected CameraController cameraController = Camera.main.GetComponent<CameraController>();
    protected Phase.PhaseStatus currentPhase;
    public Phase.PhaseStatus CurrentPhase {
        get { return currentPhase; }
        set { currentPhase = value; }
    }

    public State(string name, StateManager stateMachine) {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
