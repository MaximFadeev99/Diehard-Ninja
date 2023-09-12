using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySuperState : SuperState
{
    public DeflectState DeflectState;
    public DashState DashState;

    public AbilitySuperState(StateMachine stateMachine) : base(stateMachine)
    {
        DeflectState = new DeflectState(Player, "Deflect");
        DashState = new DashState(Player, "Dash");
        DefaultState = DeflectState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        return base.SetState();
    }
}
