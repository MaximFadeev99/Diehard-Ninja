using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuperState
{
    public State DefaultState { get; protected set; }
    public StateMachine StateMachine;
    //protected Player Player;
    public State CurrentState { get; protected set; } 

    public SuperState(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
        //Player = stateMachine.Player;
    }

    public virtual State SetState() 
    {
        State newState;

        newState = CurrentState.TryChange();

        if (newState != StateMachine.CurrentState)
        {
            StateMachine.CurrentState.Exit();
            newState.Enter();
            CurrentState = newState;
        }

        return newState;
    }
}
