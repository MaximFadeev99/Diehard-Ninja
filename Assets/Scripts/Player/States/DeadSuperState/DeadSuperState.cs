using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSuperState : SuperState
{
    public DeadState DeadState { get; private set; }
    
    public DeadSuperState(StateMachine stateMachine) : base(stateMachine)
    {
        DeadState = new DeadState(Player, "Die");
        DefaultState = DeadState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        return base.SetState();
    }

}
