using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSuperState : PlayerSuperState
{
    public DeadState DeadState { get; private set; }
    
    public DeadSuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        DeadState = new DeadState(stateMachine, "Die");
        DefaultState = DeadState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        return base.SetState();
    }

}
