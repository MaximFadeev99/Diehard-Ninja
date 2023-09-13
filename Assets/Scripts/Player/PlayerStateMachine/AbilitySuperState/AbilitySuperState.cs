using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySuperState : PlayerSuperState
{
    public DeflectState DeflectState;
    public DashState DashState;

    public AbilitySuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        DeflectState = new DeflectState(stateMachine, "Deflect");
        DashState = new DashState(stateMachine, "Dash");
        DefaultState = DeflectState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        return base.SetState();
    }
}
