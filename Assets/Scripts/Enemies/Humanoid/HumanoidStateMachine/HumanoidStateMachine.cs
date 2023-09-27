using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanoidNS;

public class HumanoidStateMachine : StateMachine
{
    public Humanoid Humanoid { get; private set; }
    public GroundedSuperState GroundedSuperState { get; private set; }
    public HumanoidNS.DeadSuperState DeadSuperState { get; private set; }

    public HumanoidStateMachine(Humanoid humanoid)
    {
        Humanoid = humanoid;
        GroundedSuperState = new GroundedSuperState(this, Humanoid);
        DeadSuperState = new HumanoidNS.DeadSuperState(this, Humanoid);
        Reset();
    }

    public override void DoLogicUpdate()
    {
        if (Humanoid.IsDead)
        {
            CallSuperState(DeadSuperState);
        }
        else
        {
            CallSuperState(GroundedSuperState);
        }
    }

    public override void Reset()
    {
        CurrentSuperState = GroundedSuperState;
        CurrentState = GroundedSuperState.SearchState;
        CurrentState.Enter();
    }
}
