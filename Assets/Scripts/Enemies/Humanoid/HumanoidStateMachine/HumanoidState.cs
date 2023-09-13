using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanoidState : State
{
    public HumanoidStateMachine HumanoidStateMachine { get; protected set; }

    public HumanoidState(HumanoidStateMachine humanoidStateMachine) 
    {
        HumanoidStateMachine = humanoidStateMachine;
    }
    
    public override void Enter() {}

    public override void Exit() {}

    public override State TryChange()
    {
        return null;
    }

    public override void UpdatePhysicalMotion() {}
}
