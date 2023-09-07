using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornSuperState : SuperState
{
    public FallState FallState { get; private set; }
    public AirbornSuperState(StateMachine stateMachine) : base(stateMachine)
    {
        FallState = new FallState(Player, "InAir");
        DefaultState = FallState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        State newState;

        newState = CurrentState.TryChange();

        if (newState != StateMachine.CurrentState)
        {
            newState.Enter();
            CurrentState = newState;
        }

        return newState;      
    }
}
