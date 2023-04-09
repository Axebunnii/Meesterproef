using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
    public string name;
    protected StateManager stateManager;
    protected CardManager cardManager;
    protected UIManager uiManager;
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
        stateManager = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Update() {
        if (currentPhase == Phase.PhaseStatus.draw) {
            phase.EnterDraw(this);
        } else if (currentPhase == Phase.PhaseStatus.card) {
            phase.PlayCard(this);
        } else if (currentPhase == Phase.PhaseStatus.shoot) {
            phase.Shoot();
        }
    }

    public virtual void Exit() { }
}
