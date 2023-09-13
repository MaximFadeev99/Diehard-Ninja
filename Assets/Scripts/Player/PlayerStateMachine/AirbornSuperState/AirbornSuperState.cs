using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornSuperState : PlayerSuperState
{
    public FallState FallState { get; private set; }
    public AirbornSuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        FallState = new FallState(stateMachine, "Fall");
        DefaultState = FallState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        return base.SetState();
    }
}
