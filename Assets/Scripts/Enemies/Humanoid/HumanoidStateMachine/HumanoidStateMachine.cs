using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanoidNS;

public class HumanoidStateMachine : StateMachine
{
    public Humanoid Humanoid { get; private set; }
    public GroundedSuperState GroundedSuperState { get; private set; }

    public HumanoidStateMachine(Humanoid humanoid)
    {
        Humanoid = humanoid;
        GroundedSuperState = new GroundedSuperState(this, Humanoid);
    }

    public override void DoLogicUpdate()
    {
        
    }

    public override void Reset()
    {
        
    }
}