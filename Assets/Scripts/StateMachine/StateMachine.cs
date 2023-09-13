using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine 
{
    public SuperState CurrentSuperState { get; protected set; }
    public State CurrentState { get; protected set; }

    public abstract void DoLogicUpdate();
    public abstract void Reset();
    public void DoPhysicsUpdate() => CurrentSuperState.CurrentState.UpdatePhysicalMotion();
    protected void ChangeSuperState(SuperState newSuperState)
    {
        CurrentSuperState = newSuperState;
        CurrentState = CurrentSuperState.SetState();
    }
}
